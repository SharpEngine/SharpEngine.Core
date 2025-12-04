using SharpEngine.Core.Manager;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace SharpEngine.Core.Utils.Tween
{
    internal class TweenData<D, DStep> where D : notnull
    {
        public object Obj { get; set; }
        public Expression<Func<object, D>> Property { get; set; }
        public D From { get; set; }
        public D To { get; set; }
        public float Duration { get; set; }
        public D CurrentValue { get; set; } = default!;
        public DStep Step { get; set; } = default!;

        public bool UseFrom { get; set; }

        public TweenData(object obj, Expression<Func<object, D>> property, D from, D to, float duration, bool useFrom = true)
        {
            Obj = obj;
            Property = property;
            From = from;
            To = to;
            Duration = duration;
            UseFrom = useFrom;
        }

        public void Launch() 
        { 
            CurrentValue = UseFrom ? From : Property.Compile()(Obj);
            Step = CurrentValue switch
            {
                float fFrom when To is float fTo => (DStep)(object)((fTo - fFrom) / Duration),
                int iFrom when To is int iTo => (DStep)(object)((iTo - iFrom) / Duration),
                Color cFrom when To is Color cTo => (DStep)(object)new ColorStep(
                    (short)((cTo.R - cFrom.R) / Duration),
                    (short)((cTo.G - cFrom.G) / Duration),
                    (short)((cTo.B - cFrom.B) / Duration),
                    (short)((cTo.A - cFrom.A) / Duration)
                ),
                _ => throw new NotSupportedException("Unsupported type for tweening.")
            };

            DebugManager.Log(LogLevel.LogInfo, $"Tween Launched: From = {From}, To = {To}, Duration = {Duration}, Step = {Step}");
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
                float cVal when Step is float step => (D)(object)(cVal + step * deltaTime),
                int cVal when Step is int step => (D)(object)(cVal + (int)(step * deltaTime)),
                Color cVal when Step is ColorStep step => (D)(object)new Color(
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
