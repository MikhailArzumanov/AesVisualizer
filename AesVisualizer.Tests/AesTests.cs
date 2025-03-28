using AesService;
using Math;

namespace AesVisualizer.Tests;

[TestClass]
public sealed class AesTests {
    [TestMethod]
    public void TestKey() {
        var key = new byte[16] {
            0x30, 0x48, 0x70, 0x71,
            0x0e, 0x14, 0x20, 0x21,
            0x22, 0x23, 0x30, 0x33,
            0x34, 0x38, 0x39, 0x40,
        };

        var expander = new KeyExpander();
        List<string> records;
        var expKey = expander.BuildKey(out records, key, 10);
        Assert.IsNotNull(expKey);
        Assert.IsTrue(expKey[0] == 0x30487071);
        Assert.IsTrue(expKey[1] == 0x0e142021);
        Assert.IsTrue(expKey[2] == 0x22233033);
        Assert.IsTrue(expKey[3] == 0x34383940);
    }

    [TestMethod]
    public void TestSubBytes() {
        var state = new byte[16];
        BytesSubstitutor.Process(ref state);
        Assert.IsNotNull(state);
        Assert.AreEqual(16, state.Length);
        Assert.IsTrue(state.All(x => x == 0x63));
    }

    [TestMethod]
    public void TestShifting() {
        var state = new byte[16] {
            0x30, 0x48, 0x70, 0x71,
            0x0e, 0x14, 0x20, 0x21,
            0x22, 0x23, 0x30, 0x33,
            0x34, 0x38, 0x39, 0x40,
        };
        var nextBytes = new byte[16];
        RowsShifter.Process(ref nextBytes, ref state);
        Assert.IsNotNull(nextBytes);
        Assert.IsNotNull(state);
        Assert.AreEqual(16, state.Length);
        Assert.AreEqual(16, nextBytes.Length);
        Assert.IsTrue(state[4] == nextBytes[ 4]);
        Assert.IsTrue(state[1] == nextBytes[13]);
        Assert.IsTrue(state[2] == nextBytes[10]);
        Assert.IsTrue(state[3] == nextBytes[ 7]);
    }

    [TestMethod]
    public void TestMixing() {
        var state = new byte[16] {
            0x30, 0x48, 0x70, 0x71,
            0x0e, 0x14, 0x20, 0x21,
            0x22, 0x23, 0x30, 0x33,
            0x34, 0x38, 0x39, 0x40,
        };
        ColumnsMixer.Process(ref state);
        var firstElement = 2*0x30 ^ 3*0x48 ^ 0x70 ^ 0x71;
        Assert.AreEqual(firstElement, state[0]);
    }
}