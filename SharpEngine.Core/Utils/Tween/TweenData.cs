using SharpEngine.Core.Manager;
using System;
using System.Linq.Expressions;
using System.Reflection;
using JetBrains.Annotations;

namespace SharpEngine.Core.Utils.Tween
{
    internal class TweenData<TD, TDStep>(
        object obj,
        Expression<Func<object, TD>> property,
        TD from,
        TD to,
        float duration,
        bool useFrom = true)
        where TD : notnull
    {
        [UsedImplicitly]
        public object Obj { get; set; } = obj;

        [UsedImplicitly]
        public Expression<Func<object, TD>> Property { get; set; } = property;

        [UsedImplicitly]
        public TD From { get; set; } = from;

        [UsedImplicitly]
        public TD To { get; set; } = to;

        [UsedImplicitly]
        public float Duration { get; set; } = duration;

        [UsedImplicitly]
        public TD CurrentValue { get; set; } = default!;
        [UsedImplicitly]
        public TDStep Step { get; set; } = default!;
        [UsedImplicitly]
        public bool UseFrom { get; set; } = useFrom;

        public void Launch() 
        { 
            CurrentValue = UseFrom ? From : Property.Compile()(Obj);
            Step = CurrentValue switch
            {
                float fFrom when To is float fTo => (TDStep)(object)((fTo - fFrom) / Duration),
                int iFrom when To is int iTo => (TDStep)(object)((iTo - iFrom) / Duration),
                Color cFrom when To is Color cTo => (TDStep)(object)new ColorStep(
                    (short)((cTo.R - cFrom.R) / Duration),
                    (short)((cTo.G - cFrom.G) / Duration),
                    (short)((cTo.B - cFrom.B) / Duration),
                    (short)((cTo.A - cFrom.A) / Duration)
                ),
                _ => throw new NotSupportedException("Unsupported type for tweening.")
            };

            DebugManager.Log(LogLevel.Debug, $"Tween Launched: From = {From}, To = {To}, Duration = {Duration}, Step = {Step}");
        }

        public void Update(float deltaTime)
        {
            if (Duration <= 0)
            {
                ((PropertyInfo)((MemberExpression)Property.Body).Member).SetValue(Obj, To);
                return;
            }

            CurrentValue = CurrentValue switch
            {
                float cVal when Step is float step => (TD)(object)(cVal + step * deltaTime),
                int cVal when Step is int step => (TD)(object)(cVal + (int)(step * deltaTime)),
                Color cVal when Step is ColorStep step => (TD)(object)new Color(
                    (byte)(cVal.R + step.RStep * deltaTime),
                    (byte)(cVal.G + step.GStep * deltaTime),
                    (byte)(cVal.B + step.BStep * deltaTime),
                    (byte)(cVal.A + step.AStep * deltaTime)
                ),
                _ => CurrentValue
            };

            ((PropertyInfo)((MemberExpression)Property.Body).Member).SetValue(Obj, CurrentValue);
            Duration -= deltaTime;
        }
    }
}
