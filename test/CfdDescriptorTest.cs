using Xunit;
using Xunit.Abstractions;

namespace Cfd.xTests
{
  public class CfdDescriptorTest
  {
    private readonly ITestOutputHelper output;

    public CfdDescriptorTest(ITestOutputHelper output)
    {
      this.output = output;
    }

    [Fact]
    public void DescriptorPkhTest()
    {
      string desc = "pkh(02c6047f9441ed7d6d3045406e95c07cd85c778e4b8cef3ca7abac09b95c709ee5)";
      Descriptor descriptor = new Descriptor(desc, CfdNetworkType.Mainnet);
      output.WriteLine("desc: " + descriptor.ToString());
      output.WriteLine("addr: " + descriptor.GetAddress().ToAddressString());
      Assert.Equal("pkh(02c6047f9441ed7d6d3045406e95c07cd85c778e4b8cef3ca7abac09b95c709ee5)#8fhd9pwu", descriptor.ToString());
      Assert.Equal(CfdHashType.P2pkh, descriptor.GetHashType());
      Assert.Equal("1cMh228HTCiwS8ZsaakH8A8wze1JR5ZsP", descriptor.GetAddress().ToAddressString());
      Assert.Equal("OP_DUP OP_HASH160 06afd46bcdfd22ef94ac122aa11f241244a37ecc OP_EQUALVERIFY OP_CHECKSIG",
        descriptor.GetAddress().GetLockingScript().GetAsm());
      Assert.Equal(CfdDescriptorKeyType.Public,
        descriptor.GetKeyData().KeyType);
      Assert.Equal("02c6047f9441ed7d6d3045406e95c07cd85c778e4b8cef3ca7abac09b95c709ee5",
        descriptor.GetKeyData().Pubkey.ToHexString());
    }

    [Fact]
    public void DescriptorWpkhTest()
    {
      string desc = "wpkh(02f9308a019258c31049344f85f89d5229b531c845836f99b08601f113bce036f9)";
      Descriptor descriptor = new Descriptor(desc, CfdNetworkType.Mainnet);
      output.WriteLine("desc: " + descriptor.ToString());
      output.WriteLine("addr: " + descriptor.GetAddress().ToAddressString());
      Assert.Equal("wpkh(02f9308a019258c31049344f85f89d5229b531c845836f99b08601f113bce036f9)#8zl0zxma", descriptor.ToString());
      Assert.Equal(CfdHashType.P2wpkh, descriptor.GetHashType());
      Assert.Equal("bc1q0ht9tyks4vh7p5p904t340cr9nvahy7u3re7zg", descriptor.GetAddress().ToAddressString());
      Assert.Equal("OP_0 7dd65592d0ab2fe0d0257d571abf032cd9db93dc",
        descriptor.GetAddress().GetLockingScript().GetAsm());
      Assert.Equal(CfdDescriptorKeyType.Public,
        descriptor.GetKeyData().KeyType);
      Assert.Equal("02f9308a019258c31049344f85f89d5229b531c845836f99b08601f113bce036f9",
        descriptor.GetKeyData().Pubkey.ToHexString());
    }

    [Fact]
    public void DescriptorShWpkhTest()
    {
      string desc = "sh(wpkh(03fff97bd5755eeea420453a14355235d382f6472f8568a18b2f057a1460297556))";
      Descriptor descriptor = new Descriptor(desc, CfdNetworkType.Mainnet);
      output.WriteLine("desc: " + descriptor.ToString());
      output.WriteLine("addr: " + descriptor.GetAddress().ToAddressString());
      output.WriteLine("asm: " + descriptor.GetAddress().GetLockingScript().GetAsm());
      Assert.Equal("sh(wpkh(03fff97bd5755eeea420453a14355235d382f6472f8568a18b2f057a1460297556))#qkrrc7je", descriptor.ToString());
      Assert.Equal(CfdHashType.P2shP2wpkh, descriptor.GetHashType());
      Assert.Equal("3LKyvRN6SmYXGBNn8fcQvYxW9MGKtwcinN", descriptor.GetAddress().ToAddressString());
      Assert.Equal("OP_HASH160 cc6ffbc0bf31af759451068f90ba7a0272b6b332 OP_EQUAL",
        descriptor.GetAddress().GetLockingScript().GetAsm());
      Assert.Equal(CfdDescriptorKeyType.Public,
        descriptor.GetKeyData().KeyType);
      Assert.Equal("03fff97bd5755eeea420453a14355235d382f6472f8568a18b2f057a1460297556",
        descriptor.GetKeyData().Pubkey.ToHexString());
    }

    [Fact]
    public void DescriptorShMultiTest()
    {













      string addrStr = "bcrt1q576jgpgewxwu205cpjq4s4j5tprxlq38l7kd85";
      Cfd.Address addr = new Cfd.Address(addrStr);
      Assert.Equal(addrStr, addr.ToAddressString());
      Assert.Equal("0014a7b5240519719dc53e980c8158565458466f8227", addr.GetLockingScript().ToHexString());
      Assert.Equal("a7b5240519719dc53e980c8158565458466f8227", addr.GetHash());

      Cfd.Pubkey pubkey = new Cfd.Pubkey("031d7463018f867de51a27db866f869ceaf52abab71827a6051bab8a0fd020f4c1");
      addr = new Cfd.Address(pubkey, Cfd.CfdAddressType.P2wpkh, Cfd.CfdNetworkType.ElementsRegtest);
      Assert.Equal("ert1q7jm5vw5cunpy3lkvwdl3sr3qfm794xd4zr6z3k", addr.ToAddressString());
      Assert.Equal("0014f4b7463a98e4c248fecc737f180e204efc5a99b5", addr.GetLockingScript().ToHexString());
    }

    [Fact]
    public void DescriptorWshMultiTest()
    {
      string addrStr = "bcrt1q576jgpgewxwu205cpjq4s4j5tprxlq38l7kd85";
      Cfd.Address addr = new Cfd.Address(addrStr);
      Assert.Equal(addrStr, addr.ToAddressString());
      Assert.Equal("0014a7b5240519719dc53e980c8158565458466f8227", addr.GetLockingScript().ToHexString());
      Assert.Equal("a7b5240519719dc53e980c8158565458466f8227", addr.GetHash());

      Cfd.Pubkey pubkey = new Cfd.Pubkey("031d7463018f867de51a27db866f869ceaf52abab71827a6051bab8a0fd020f4c1");
      addr = new Cfd.Address(pubkey, Cfd.CfdAddressType.P2wpkh, Cfd.CfdNetworkType.ElementsRegtest);
      Assert.Equal("ert1q7jm5vw5cunpy3lkvwdl3sr3qfm794xd4zr6z3k", addr.ToAddressString());
      Assert.Equal("0014f4b7463a98e4c248fecc737f180e204efc5a99b5", addr.GetLockingScript().ToHexString());
    }


    [Fact]
    public void DescriptorShWshMultiTest()
    {
      string addrStr = "bcrt1q576jgpgewxwu205cpjq4s4j5tprxlq38l7kd85";
      Cfd.Address addr = new Cfd.Address(addrStr);
      Assert.Equal(addrStr, addr.ToAddressString());
      Assert.Equal("0014a7b5240519719dc53e980c8158565458466f8227", addr.GetLockingScript().ToHexString());
      Assert.Equal("a7b5240519719dc53e980c8158565458466f8227", addr.GetHash());

      Cfd.Pubkey pubkey = new Cfd.Pubkey("031d7463018f867de51a27db866f869ceaf52abab71827a6051bab8a0fd020f4c1");
      addr = new Cfd.Address(pubkey, Cfd.CfdAddressType.P2wpkh, Cfd.CfdNetworkType.ElementsRegtest);
      Assert.Equal("ert1q7jm5vw5cunpy3lkvwdl3sr3qfm794xd4zr6z3k", addr.ToAddressString());
      Assert.Equal("0014f4b7463a98e4c248fecc737f180e204efc5a99b5", addr.GetLockingScript().ToHexString());
    }

    [Fact]
    public void DescriptorAddrTest()
    {
      string addrStr = "bcrt1q576jgpgewxwu205cpjq4s4j5tprxlq38l7kd85";
      Cfd.Address addr = new Cfd.Address(addrStr);
      Assert.Equal(addrStr, addr.ToAddressString());
      Assert.Equal("0014a7b5240519719dc53e980c8158565458466f8227", addr.GetLockingScript().ToHexString());
      Assert.Equal("a7b5240519719dc53e980c8158565458466f8227", addr.GetHash());

      Cfd.Pubkey pubkey = new Cfd.Pubkey("031d7463018f867de51a27db866f869ceaf52abab71827a6051bab8a0fd020f4c1");
      addr = new Cfd.Address(pubkey, Cfd.CfdAddressType.P2wpkh, Cfd.CfdNetworkType.ElementsRegtest);
      Assert.Equal("ert1q7jm5vw5cunpy3lkvwdl3sr3qfm794xd4zr6z3k", addr.ToAddressString());
      Assert.Equal("0014f4b7463a98e4c248fecc737f180e204efc5a99b5", addr.GetLockingScript().ToHexString());
    }


    [Fact]
    public void DescriptorRawTest()
    {
      string addrStr = "bcrt1q576jgpgewxwu205cpjq4s4j5tprxlq38l7kd85";
      Cfd.Address addr = new Cfd.Address(addrStr);
      Assert.Equal(addrStr, addr.ToAddressString());
      Assert.Equal("0014a7b5240519719dc53e980c8158565458466f8227", addr.GetLockingScript().ToHexString());
      Assert.Equal("a7b5240519719dc53e980c8158565458466f8227", addr.GetHash());

      Cfd.Pubkey pubkey = new Cfd.Pubkey("031d7463018f867de51a27db866f869ceaf52abab71827a6051bab8a0fd020f4c1");
      addr = new Cfd.Address(pubkey, Cfd.CfdAddressType.P2wpkh, Cfd.CfdNetworkType.ElementsRegtest);
      Assert.Equal("ert1q7jm5vw5cunpy3lkvwdl3sr3qfm794xd4zr6z3k", addr.ToAddressString());
      Assert.Equal("0014f4b7463a98e4c248fecc737f180e204efc5a99b5", addr.GetLockingScript().ToHexString());
    }

  }
}
