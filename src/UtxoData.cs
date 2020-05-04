using System;
using System.Security.Cryptography;

/// <summary>
/// cfd library namespace.
/// </summary>
namespace Cfd
{
  /// <summary>
  /// txid data class.
  /// </summary>
  public class UtxoData : IEquatable<UtxoData>
  {
    private readonly OutPoint outpoint;
    private readonly long amount;
    private readonly Descriptor descriptor;

    /// <summary>
    /// Constructor. (empty)
    /// </summary>
    public UtxoData()
    {
      // do nothing
    }

    public UtxoData(OutPoint outpoint)
    {
      if (outpoint is null)
      {
        throw new ArgumentNullException(nameof(outpoint));
      }
      this.outpoint = outpoint;
    }

    public UtxoData(OutPoint outpoint, Descriptor descriptor)
    {
      if (outpoint is null)
      {
        throw new ArgumentNullException(nameof(outpoint));
      }
      if (descriptor is null)
      {
        throw new ArgumentNullException(nameof(descriptor));
      }
      this.outpoint = outpoint;
      this.descriptor = descriptor;
    }

    public UtxoData(OutPoint outpoint, long amount)
    {
      if (outpoint is null)
      {
        throw new ArgumentNullException(nameof(outpoint));
      }
      this.outpoint = outpoint;
      this.amount = amount;
    }

    public UtxoData(OutPoint outpoint, long amount, Descriptor descriptor)
    {
      if (outpoint is null)
      {
        throw new ArgumentNullException(nameof(outpoint));
      }
      if (descriptor is null)
      {
        throw new ArgumentNullException(nameof(descriptor));
      }
      this.outpoint = outpoint;
      this.amount = amount;
      this.descriptor = descriptor;
    }

    public OutPoint GetOutPoint()
    {
      return outpoint;
    }

    public long GetAmount()
    {
      return amount;
    }

    public Descriptor GetDescriptor()
    {
      return descriptor;
    }

    public bool Equals(UtxoData other)
    {
      if (other is null)
      {
        return false;
      }
      if (Object.ReferenceEquals(this, other))
      {
        return true;
      }

      return (outpoint == other.outpoint);
    }

    public override bool Equals(object obj)
    {
      if (obj is null)
      {
        return false;
      }
      if ((obj as UtxoData) != null)
      {
        return this.Equals((UtxoData)obj);
      }
      return false;
    }

    public override int GetHashCode()
    {
      return HashCode.Combine(outpoint);
    }

    public static bool operator ==(UtxoData lhs, UtxoData rhs)
    {
      if (lhs is null)
      {
        if (rhs is null)
        {
          return true;
        }
        return false;
      }
      return lhs.Equals(rhs);
    }

    public static bool operator !=(UtxoData lhs, UtxoData rhs)
    {
      return !(lhs == rhs);
    }
  }
}
