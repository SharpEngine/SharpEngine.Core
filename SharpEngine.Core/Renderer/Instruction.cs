using System.Collections.Generic;

namespace SharpEngine.Core.Renderer;

/// <summary>
/// Struct which represents renderer instruction
/// </summary>
public struct Instruction
{
    /// <summary>
    /// Type of Instruction
    /// </summary>
    public InstructionType Type;

    /// <summary>
    /// if Instruction is for entities or ui
    /// </summary>
    public InstructionSource Source;

    /// <summary>
    /// Z Layer of Instruction
    /// </summary>
    public float ZLayer;

    /// <summary>
    /// Parameters of Instruction
    /// </summary>
    public List<object> Parameters;
}
