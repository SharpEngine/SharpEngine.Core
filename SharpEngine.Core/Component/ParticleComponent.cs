using System.Collections.Generic;
using JetBrains.Annotations;
using SharpEngine.Core.Particle;

namespace SharpEngine.Core.Component;

/// <summary>
/// Component which can display particles
/// </summary>
[UsedImplicitly]
public class ParticleComponent : Component
{
    /// <summary>
    /// Particle Emitters
    /// </summary>
    [UsedImplicitly]
    public List<ParticleEmitter> ParticleEmitters { get; } = [];

    /// <summary>
    /// Represents the associated transform component for the current object, if available.
    /// </summary>
    [UsedImplicitly]
    protected TransformComponent? Transform;

    /// <inheritdoc />
    public override void Load()
    {
        base.Load();

        Transform = Entity?.GetComponentAs<TransformComponent>();
    }

    /// <inheritdoc />
    public override void Update(float delta)
    {
        base.Update(delta);

        if (Transform == null)
            return;

        foreach (var emitter in ParticleEmitters)
            emitter.Update(delta, Transform.Position);
    }

    /// <inheritdoc />
    public override void Draw()
    {
        base.Draw();

        if (Transform == null)
            return;

        foreach (var emitter in ParticleEmitters)
            emitter.Draw();
    }
}
