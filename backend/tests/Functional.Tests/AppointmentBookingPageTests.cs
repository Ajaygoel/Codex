using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Xunit;

namespace Functional.Tests;

public class AppointmentBookingPageTests
{
    [Fact]
    public void LandingPageShowsBookingHeader()
    {
        var baseUrl = Environment.GetEnvironmentVariable("E2E_BASE_URL") ?? "http://localhost:4200";
        var options = new ChromeOptions();
        options.AddArgument("--headless=new");
        options.AddArgument("--no-sandbox");
        options.AddArgument("--disable-dev-shm-usage");

        using var driver = new ChromeDriver(options);
        driver.Navigate().GoToUrl(baseUrl);

        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        var header = wait.Until(webDriver => webDriver.FindElement(By.CssSelector("h1")));

        Assert.Equal("Doctor Appointment Booking", header.Text);
    }
}
