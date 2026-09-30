using System;
using JetBrains.Annotations;

namespace SharpEngine.Core.Utils.Tween
{
	public abstract class ITweenData
	{
		public abstract void Launch();
		public abstract void Update(float t);
	}

	public abstract class TweenData<T> : ITweenData
	{
		[UsedImplicitly]
		public Func<T> Getter { get; set; } = default!;

		[UsedImplicitly]
		public Action<T> Setter { get; set; } = default!;

		[UsedImplicitly]
		public T From { get; set; } = default!;

		[UsedImplicitly]
		public T To { get; set; } = default!;

		internal bool UseCurrentValue { get; set; }

		public sealed override void Launch()
		{
			if (UseCurrentValue)
				From = Getter();
		}
	}

	internal sealed class IntTweenData : TweenData<int>
	{
		public override void Update(float t)
		{
			Setter(t >= 1f ? To : (int)(From + ((double)To - From) * t));
		}
	}

	internal sealed class FloatTweenData : TweenData<float>
	{
		public override void Update(float t)
		{
			Setter(t >= 1f ? To : From + (To - From) * t);
		}
	}

	internal sealed class ColorTweenData : TweenData<Color>
	{
		public override void Update(float t)
		{
			Setter(
				new Color(
					(int)(From.R + (To.R - From.R) * t),
					(int)(From.G + (To.G - From.G) * t),
					(int)(From.B + (To.B - From.B) * t),
					(int)(From.A + (To.A - From.A) * t)
				)
			);
		}
	}
}
