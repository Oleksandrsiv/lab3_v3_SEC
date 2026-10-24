using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;

namespace lab3_v3_SEC
{
    public class Variant3Tests : BaseTest
    {
       [Fact]
        public void Checkboxes_ClickUnchecked_BecomesChecked()
        {
            // Arrange
            OpenPage("checkboxes");
            var firstCheckbox = driver.FindElement(By.CssSelector("form#checkboxes input[type='checkbox']:first-child"));
            
            // Act
            firstCheckbox.Click();
            
            // Assert
            Assert.True(firstCheckbox.Selected);
        }

        [Fact]
        public void AddRemoveElements_AddAndDelete_Success()
        {
            // Arrange
            OpenPage("add_remove_elements/");
            var addButton = driver.FindElement(By.XPath("//button[text()='Add Element']"));

            // Act 1
            addButton.Click();
            var deleteButton = driver.FindElement(By.ClassName("added-manually"));

            // Assert 1
            Assert.True(deleteButton.Displayed);

            // Act 2
            deleteButton.Click();

            // Assert 2
            var remainingDeleteButtons = driver.FindElements(By.ClassName("added-manually"));
            Assert.Empty(remainingDeleteButtons);
        }

        [Fact]
        public void Dropdown_SelectOption_OptionIsSelected()
        {
            // Arrange
            OpenPage("dropdown");
            var dropdownElement = driver.FindElement(By.Id("dropdown"));
            var select = new SelectElement(dropdownElement);

            // Act
            select.SelectByText("Option 1");

            // Assert
            Assert.Equal("Option 1", select.SelectedOption.Text);
        }

        [Fact]
        public void Inputs_SendNumericValue_ValueIsAccepted()
        {
            // Arrange
            OpenPage("inputs");
            var inputField = driver.FindElement(By.CssSelector("input[type='number']"));

            // Act
            inputField.SendKeys("42");

            // Assert
            Assert.Equal("42", inputField.GetAttribute("value"));
        }

        [Fact]
        public void StatusCodes_Click200Link_Displays200Message()
        {
            // Arrange
            OpenPage("status_codes");
            var link200 = driver.FindElement(By.LinkText("200"));

            // Act
            link200.Click();

            // Assert
            var message = driver.FindElement(By.TagName("p")).Text;
            Assert.Contains("This page returned a 200 status code", message);
        }

        [Fact]
        public void NestedFrames_SwitchToLeftFrame_ReadsText()
        {
            // Arrange
            OpenPage("nested_frames");

            // Act
            driver.SwitchTo().Frame("frame-top");
            driver.SwitchTo().Frame("frame-left");
            var bodyText = driver.FindElement(By.TagName("body")).Text;

            // Assert
            Assert.Equal("LEFT", bodyText);
        }

        [Fact]
        public void FormAuthentication_ValidCredentials_LoginSuccessful()
        {
            // Arrange
            OpenPage("login");
            var usernameInput = driver.FindElement(By.Id("username"));
            var passwordInput = driver.FindElement(By.Id("password"));
            var loginButton = driver.FindElement(By.CssSelector("button[type='submit']"));

            // Act
            usernameInput.SendKeys("tomsmith");
            passwordInput.SendKeys("SuperSecretPassword!");
            loginButton.Click();

            // Assert
            var flashMessage = driver.FindElement(By.Id("flash")).Text;
            Assert.Contains("You logged into a secure area!", flashMessage);
        }

        [Fact]
        public void DynamicContent_RefreshPage_ContentChanges()
        {
            // Arrange
            OpenPage("dynamic_content");
            var initialText = driver.FindElement(By.Id("content")).Text;

            // Act
            driver.Navigate().Refresh();

            // Assert
            var refreshedText = driver.FindElement(By.Id("content")).Text;
            Assert.NotEqual(initialText, refreshedText);
        }

        [Fact]
        public void DisappearingElements_CountMenuItems_ReturnsFourOrFive()
        {
            // Arrange
            OpenPage("disappearing_elements");

            // Act
            var menuItems = driver.FindElements(By.CssSelector("ul li"));

            // Assert
            Assert.True(menuItems.Count == 4 || menuItems.Count == 5);
        }

        [Fact]
        public void Hovers_HoverOverAvatar_DisplaysProfileLink()
        {
            // Arrange
            OpenPage("hovers");
            var avatar = driver.FindElement(By.ClassName("figure"));
            var actions = new Actions(driver);

            // Act
            actions.MoveToElement(avatar).Perform();

            // Assert
            var caption = avatar.FindElement(By.ClassName("figcaption"));
            Assert.True(caption.Displayed);
            Assert.Contains("name: user1", caption.Text);
        }
    }
}