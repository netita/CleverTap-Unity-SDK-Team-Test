using NUnit.Framework;

[TestFixture]
public class ToastModelTests
{
    [Test]
    public void ToastModel_DefaultValues_AreCorrect()
    {
        // Arrange
        ToastModel toastModel = new ToastModel();

        // Act and Assert
        Assert.AreEqual(string.Empty, toastModel.Text);
        Assert.AreEqual(3f, toastModel.Duration);
        Assert.AreEqual(ToastPosition.BottomCenter, toastModel.Position);
    }

    [Test]
    public void ToastModel_ParameterizedConstructor_SetsCorrectValues()
    {
        // Arrange
        var text = "Test Toast";
        var duration = 5f;
        var position = ToastPosition.TopCenter;
        ToastModel toastModel = new ToastModel(text, duration, position);

        // Act and Assert
        Assert.AreEqual(text, toastModel.Text);
        Assert.AreEqual(duration, toastModel.Duration);
        Assert.AreEqual(position, toastModel.Position);
    }
}
