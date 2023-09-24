using System.Collections.Generic;
using SharpEngine.Core.Particle;

namespace SharpEngine.Core.Component;

/// <summary>
/// Component which can display particles
/// </summary>
public class ParticleComponent : Component
{
    /// <summary>
    /// Particle Emitters
    /// </summary>
    public List<ParticleEmitter> ParticleEmitters { get; } = new();

    private TransformComponent? _transform;

    /// <inheritdoc />
    public override void Load()
    {
        base.Load();

        _transform = Entity?.GetComponentAs<TransformComponent>();
    }

    /// <inheritdoc />
    public override void Update(float delta)
    {
        base.Update(delta);

        if (_transform == null)
            return;

        foreach (var emitter in ParticleEmitters)
            emitter.Update(delta, _transform.GetTransformedPosition());
    }

    /// <inheritdoc />
    public override void Draw()
    {
        base.Draw();

        if (_transform == null)
            return;

        foreach (var emitter in ParticleEmitters)
            emitter.Draw();
    }
}
