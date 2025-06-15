# UI Automation Framework – IncubyteAutomation

This project is a UI test automation framework using **SpecFlow**, **Selenium WebDriver**, and **NUnit**, following the **Page Object Model**. 
It supports dynamic test configuration using `.runsettings` and generates test execution reports via **ExtentReports**.

---
## ✅ Note

### **The `Results` folder is ignored via `.gitignore`, So I have added it manually for verification and submission purposes.**

---

## 🔧 Setup Instructions

1. **Clone the Repo**  
   Pull the latest code from the main branch.

2. **Install Dependencies**  
   - Open the solution in Visual Studio.  
   - Restore NuGet packages.

3. **Configure Run Settings**  
   - Open `Test > Configure Run Settings > Select Solution Run Settings File`.  
   - Select `TestData.runsettings` located in the root folder.

4. **Execute Tests**  
   - Use Test Explorer or run via command line:  
     ```bash
     dotnet test --settings TestData.runsettings
     ```

---

## 📊 Reports

- Test execution reports are generated using **ExtentReports**.
- Output is saved in `Magneto.softwareTesting/Tests/Results/TestResults/Run_(CurrentDateTimeFolder)/index.html`.

