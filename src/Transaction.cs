using System;
/// <summary>
/// cfd library namespace.
/// </summary>
namespace Cfd
{
  /// <summary>
  /// txin sequence locktime
  /// </summary>
  public enum CfdSequenceLockTime : uint
  {
    /// disable locktime
    Disable = 0xffffffff,
    /// enable locktime (maximum time)
    EnableMax = 0xfffffffe,
  };

  /// <summary>
  /// Transaction signature hash type.
  /// </summary>
  public struct SignatureHashType
  {
    /// <summary>
    /// signature hash type.
    /// </summary>
    public CfdSighashType SighashType { get; }
    /// <summary>
    /// use signature hash anyone can pay.
    /// </summary>
    public bool IsSighashAnyoneCanPay { get; }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="sighashType">sighash type.</param>
    /// <param name="sighashAnyoneCanPay">sighash anyone can pay.</param>
    public SignatureHashType(CfdSighashType sighashType, bool sighashAnyoneCanPay)
    {
      this.SighashType = sighashType;
      this.IsSighashAnyoneCanPay = sighashAnyoneCanPay;
    }
  }

  /// <summary>
  /// Transaction input struct.
  /// </summary>
  public struct TxIn
  {
    /// <summary>
    /// outpoint.
    /// </summary>
    public OutPoint OutPoint { get; }
    /// <summary>
    /// scriptsig data.
    /// </summary>
    public Script ScriptSig { get; }
    /// <summary>
    /// sequence number.
    /// </summary>
    public UInt32 Sequence { get; }
    /// <summary>
    /// witness stack.
    /// </summary>
    public ScriptWitness WitnessStack { get; }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="outPoint">outpoint</param>
    public TxIn(OutPoint outPoint)
    {
      this.OutPoint = outPoint;
      this.ScriptSig = new Script();
      this.Sequence = (UInt32)CfdSequenceLockTime.Disable;
      this.WitnessStack = new ScriptWitness();
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="outPoint">outpoint</param>
    /// <param name="sequence">sequence number</param>
    public TxIn(OutPoint outPoint, UInt32 sequence)
    {
      this.OutPoint = outPoint;
      this.ScriptSig = new Script();
      this.Sequence = sequence;
      this.WitnessStack = new ScriptWitness();
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="outPoint">outpoint</param>
    /// <param name="scriptWitness">witness stack</param>
    public TxIn(OutPoint outPoint, ScriptWitness scriptWitness)
    {
      this.OutPoint = outPoint;
      this.ScriptSig = new Script();
      this.Sequence = (UInt32)CfdSequenceLockTime.Disable;
      this.WitnessStack = scriptWitness;
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="outPoint">outpoint</param>
    /// <param name="sequence">sequence number</param>
    /// <param name="scriptWitness">witness stack</param>
    public TxIn(OutPoint outPoint, UInt32 sequence, ScriptWitness scriptWitness)
    {
      this.OutPoint = outPoint;
      this.ScriptSig = new Script();
      this.Sequence = sequence;
      this.WitnessStack = scriptWitness;
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="outPoint">outpoint</param>
    /// <param name="scriptSig">scriptsig data</param>
    /// <param name="scriptWitness">witness stack</param>
    public TxIn(OutPoint outPoint, Script scriptSig, ScriptWitness scriptWitness)
    {
      this.OutPoint = outPoint;
      this.ScriptSig = scriptSig;
      this.Sequence = (UInt32)CfdSequenceLockTime.Disable;
      this.WitnessStack = scriptWitness;
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="outPoint">outpoint</param>
    /// <param name="sequence">sequence number</param>
    /// <param name="scriptSig">scriptsig data</param>
    /// <param name="scriptWitness">witness stack</param>
    public TxIn(OutPoint outPoint, UInt32 sequence, Script scriptSig, ScriptWitness scriptWitness)
    {
      this.OutPoint = outPoint;
      this.ScriptSig = scriptSig;
      this.Sequence = sequence;
      this.WitnessStack = scriptWitness;
    }
  };

  /// <summary>
  /// Transaction output struct.
  /// </summary>
  public struct TxOut
  {
    /// <summary>
    /// satoshi value.
    /// </summary>
    public long SatoshiValue { get; }
    /// <summary>
    /// scriptPubkey (locking script).
    /// </summary>
    public Script ScriptPubkey { get; }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="satoshiValue">satoshi value</param>
    /// <param name="scriptPubkey">locking script</param>
    public TxOut(long satoshiValue, Script scriptPubkey)
    {
      this.SatoshiValue = satoshiValue;
      this.ScriptPubkey = scriptPubkey;
    }
  };
}
