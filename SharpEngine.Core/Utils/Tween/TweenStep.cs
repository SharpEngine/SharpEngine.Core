using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using JetBrains.Annotations;

namespace SharpEngine.Core.Utils.Tween;

/// <summary>
/// Represents a single step in a tween animation sequence.
/// </summary>
/// <remarks>A TweenStep typically encapsulates the logic for updating a portion of an animation over
/// time. Instances of this class are commonly used within tweening frameworks to manage incremental changes to
/// animated properties.</remarks>
public class TweenStep
{
	/// <summary>
	/// Duration of the step
	/// </summary>
	[UsedImplicitly]
	public float Duration { get; set; }

	/// <summary>
	/// Elapsed time of the step
	/// </summary>
	[UsedImplicitly]
	public float Elapsed { get; set; }

	private readonly List<(ITweenData Data, float Duration)> _tweens = [];

	/// <summary>
	/// Initializes a new instance of the TweenStep class with the specified duration.
	/// </summary>
	/// <param name="duration">The duration, in seconds, for this tween step. Must be greater than or equal to zero.</param>
	public TweenStep(float duration)
	{
		Duration = duration;
		Elapsed = 0;
	}

	/// <summary>
	/// Add a float tween to the step
	/// </summary>
	/// <param name="entity">Entity</param>
	/// <param name="property">Property which be modified</param>
	/// <param name="to">Target Value</param>
	/// <param name="duration">Duration of the tween</param>
	/// <param name="from">Source Value</param>
	/// <returns>Tween Step</returns>
	public TweenStep Float<T>(
		T entity,
		Expression<Func<T, float>> property,
		float to,
		float duration,
		float? from = null
	)
		where T : class
	{
		return from.HasValue
			? Custom<T, FloatTweenData, float>(entity, property, from.Value, to, duration)
			: Custom<T, FloatTweenData, float>(entity, property, to, duration);
	}

	/// <summary>
	/// Add an int tween to the step
	/// </summary>
	/// <param name="entity">Entity</param>
	/// <param name="property">Property which be modified</param>
	/// <param name="to">Target Value</param>
	/// <param name="duration">Duration of the tween</param>
	///
	/// <param name="from">Source Value</param>
	/// <returns>Tween Steps</returns>
	[UsedImplicitly]
	public TweenStep Int<T>(
		T entity,
		Expression<Func<T, int>> property,
		int to,
		float duration,
		int? from = null
	)
		where T : class
	{
		return from.HasValue
			? Custom<T, IntTweenData, int>(entity, property, from.Value, to, duration)
			: Custom<T, IntTweenData, int>(entity, property, to, duration);
	}

	/// <summary>
	/// Animates a color property of the specified entity from a starting value to a target value over the given
	/// duration.
	/// </summary>
	/// <param name="entity">The entity whose color property will be animated.</param>
	/// <param name="property">An expression that selects the color property of the entity to animate.</param>
	/// <param name="to">The target color value to animate to.</param>
	/// <param name="duration">The duration, in seconds, over which the animation occurs. Must be greater than zero.</param>
	/// <param name="from">The initial color value to animate from. If null, the current value of the property is used.</param>
	/// <returns>The current <see cref="TweenStep"/> instance, allowing for method chaining.</returns>
	[UsedImplicitly]
	public TweenStep Color<T>(
		T entity,
		Expression<Func<T, Color>> property,
		Color to,
		float duration,
		Color? from = null
	)
		where T : class
	{
		return from.HasValue
			? Custom<T, ColorTweenData, Color>(entity, property, from.Value, to, duration)
			: Custom<T, ColorTweenData, Color>(entity, property, to, duration);
	}

	public TweenStep Custom<TEntity, TData, T>(
		TEntity entity,
		Expression<Func<TEntity, T>> property,
		T to,
		float duration
	)
		where TEntity : class
		where TData : TweenData<T>, new()
	{
		var compiledGetter = property.Compile();
		var compiledSetter = CompileSetter(property);
		Func<T> getter = () => compiledGetter(entity);
		Action<T> setter = value => compiledSetter(entity, value);

		return Custom<TData, T>(getter, setter, default!, to, duration, useCurrentValue: true);
	}

	public TweenStep Custom<TEntity, TData, T>(
		TEntity entity,
		Expression<Func<TEntity, T>> property,
		T from,
		T to,
		float duration
	)
		where TEntity : class
		where TData : TweenData<T>, new()
	{
		var compiledGetter = property.Compile();
		var compiledSetter = CompileSetter(property);
		Func<T> getter = () => compiledGetter(entity);
		Action<T> setter = value => compiledSetter(entity, value);

		return Custom<TData, T>(getter, setter, from, to, duration);
	}

	private TweenStep Custom<TData, T>(
		Func<T> getter,
		Action<T> setter,
		T from,
		T to,
		float duration,
		bool useCurrentValue = false
	)
		where TData : TweenData<T>, new()
	{
		_tweens.Add(
			(
				new TData
				{
					Getter = getter,
					Setter = setter,
					From = from,
					To = to,
					UseCurrentValue = useCurrentValue,
				},
				duration
			)
		);
		return this;
	}

	/// <summary>
	/// Update the tween step
	/// </summary>
	/// <param name="deltaTime">Delta time</param>
	/// <returns>If a step ended</returns>
	public bool Update(float deltaTime)
	{
		Elapsed += deltaTime;
		foreach (var tween in _tweens)
		{
			float progress =
				tween.Duration <= 0 ? 1f : System.Math.Clamp(Elapsed / tween.Duration, 0f, 1f);
			tween.Data.Update(progress);
		}

		return Elapsed >= Duration;
	}

	internal void Launch()
	{
		Elapsed = 0;
		foreach (var tween in _tweens)
			tween.Data.Launch();
	}

	private static Action<T, TValue> CompileSetter<T, TValue>(
		Expression<Func<T, TValue>> expression
	)
		where T : class
	{
		if (expression.Body is not MemberExpression)
			throw new ArgumentException(
				"Expression must be a field or property access.",
				nameof(expression)
			);

		// Extract:
		//
		// x => x.A.B.C.Value
		//
		// into:
		//
		// A, B, C, Value

		var members = new List<MemberInfo>();

		Expression? current = expression.Body;

		while (current is MemberExpression memberExpression)
		{
			members.Add(memberExpression.Member);
			current = memberExpression.Expression;
		}

		if (current != expression.Parameters[0])
			throw new ArgumentException(
				"Expression must originate from the lambda parameter.",
				nameof(expression)
			);

		members.Reverse();

		var objParameter = Expression.Parameter(typeof(T), "obj");

		var valueParameter = Expression.Parameter(typeof(TValue), "value");

		List<ParameterExpression> variables = [];
		List<Expression> body = [];

		// Structs that will need to be copied back into their parents.
		var writeBacks =
			new List<(Expression Parent, MemberInfo Member, ParameterExpression Temp)>();

		Expression owner = objParameter;

		// Walk everything except the final member.
		for (int i = 0; i < members.Count - 1; i++)
		{
			var member = members[i];

			var access = Expression.MakeMemberAccess(owner, member);

			if (access.Type.IsValueType)
			{
				/*
				 * Struct:
				 *
				 * obj.Position.X
				 *
				 * becomes roughly:
				 *
				 * var temp = obj.Position;
				 */

				var temp = Expression.Variable(access.Type, $"temp{i}");

				variables.Add(temp);

				body.Add(Expression.Assign(temp, access));

				writeBacks.Add((owner, member, temp));

				owner = temp;
			}
			else
			{
				// Classes don't need copying.
				owner = access;
			}
		}

		// Final property/field.
		var leafMember = members[^1];

		EnsureWritable(leafMember);

		var leaf = Expression.MakeMemberAccess(owner, leafMember);

		body.Add(Expression.Assign(leaf, valueParameter));

		for (int i = writeBacks.Count - 1; i >= 0; i--)
		{
			var writeBack = writeBacks[i];

			EnsureWritable(writeBack.Member);

			var destination = Expression.MakeMemberAccess(writeBack.Parent, writeBack.Member);

			body.Add(Expression.Assign(destination, writeBack.Temp));
		}

		var block = Expression.Block(variables, body);

		return Expression.Lambda<Action<T, TValue>>(block, objParameter, valueParameter).Compile();
	}

	private static void EnsureWritable(MemberInfo member)
	{
		switch (member)
		{
			case PropertyInfo { SetMethod: null } property:
				throw new ArgumentException($"Property '{property.Name}' is not writable.");

			case FieldInfo { IsInitOnly: true } field:
				throw new ArgumentException($"Field '{field.Name}' is readonly.");

			case PropertyInfo:
			case FieldInfo:
				return;

			default:
				throw new ArgumentException($"Member '{member.Name}' is not a field or property.");
		}
	}
}
