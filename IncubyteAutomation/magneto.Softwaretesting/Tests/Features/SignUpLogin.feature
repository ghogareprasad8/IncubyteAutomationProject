Feature: Sign Up Functionality
  As a new user
  I want to create an account on the website
  So that I can log in and access my profile


Background:
	Given the user is on the e-commerce website
	And the user navigates to the "Create an Account" page 
	Then the user should be navigated to "Create New Customer Account" page

@Positive @SignUp @TC001
Scenario: TC001_Successful Sign-Up With Valid Details
	When the user enters "First Name" as "firstName"
	And the user enters "Last Name" as "lastName"
	And the user enters "Email" as "uniqueEmail"
	And the user enters "Password" as "password"
	And the user enters "Confirm Password" as "password"
	And the user clicks the "Create an Account" button
	Then the user should be navigated to "My Account" page
	And the user should see message "Thank you for registering with Main Website Store."
	And the user should be logged in as "firstName" "lastName"
	When the user clicks on the Welcome user icon
	And the user clicks on "Sign Out"
	Then the user should be navigated to "You are signed out" page
	And after 5 seconds, the user should be redirected to the home page

@Negative @SignUp @TC002
Scenario: TC002_Sign-Up With Existing Email
    When the user enters "First Name" as "firstName"
    And the user enters "Last Name" as "lastName"
    And the user enters "Email" as "uniqueEmail"
    And the user enters "Password" as "password"
    And the user enters "Confirm Password" as "password"
    And the user clicks the "Create an Account" button
    Then the user should be navigated to "My Account" page
	And the user should see message "Thank you for registering with Main Website Store."
	When the user clicks on the Welcome user icon
	And the user clicks on "Sign Out"
	Then the user should be navigated to "You are signed out" page
	Given the user navigates to the "Create an Account" page 
	Then the user should be navigated to "Create New Customer Account" page
    When the user enters "First Name" as "firstName"
    And the user enters "Last Name" as "lastName"
    And the user enters "Email" as "uniqueEmail"
    And the user enters "Password" as "password"
    And the user enters "Confirm Password" as "password"
	And the user clicks the "Create an Account" button
    Then the user should see message "There is already an account with this email address"

@Negative @SignUp @TC003
Scenario: TC003_Password and Confirm Password Do Not Match
	When the user enters "First Name" as "firstName"
	And the user enters "Last Name" as "lastName"
	And the user enters "Email" as "uniqueEmail"
	And the user enters "Password" as "password"
	And the user enters "Confirm Password" as "mismatchPassword"
	And the user clicks the "Create an Account" button
	Then the user should see validation error "Please enter the same value again" for "Confirm Password" feild

@Negative @SignUp @TC004
Scenario: TC004_Sign-Up With Weak Password
	When the user enters "First Name" as "firstName"
	And the user enters "Last Name" as "lastName"
	And the user enters "Email" as "uniqueEmail"
	And the user enters "Password" as "weakPassword"
	And the user enters "Confirm Password" as "weakPassword"
	And the user clicks the "Create an Account" button
	Then the user should see password strength error message

@Negative @SignUp @TC005
Scenario: TC005_Leave All Fields Blank
	When the user clicks the "Create an Account" button
	Then the user should see validation errors for all required fields

@Negative @SignUp @TC006
Scenario Outline: TC006_Invalid Email Format
	When the user enters "First Name" as "firstName"
	And the user enters "Last Name" as "lastName"
	And the user enters "<invalidEmail>" in the email feild 
	And the user enters "Password" as "password"
	And the user enters "Confirm Password" as "password"
	And the user clicks the "Create an Account" button
	Then the user should see validation error "Please enter a valid email address" for "Email" feild
Examples:
	| invalidEmail |
	| john.doe     |
	| john@.com    |
	| @example.com |
	| test@com     |

@UI @SignUp @TC007
Scenario: TC007_Password Field Masking
	Then the password fields should be masked

@Positive @SignUp @TC008
Scenario: TC008_Page Title and URL Verification After Sign-Up
	When the user enters "First Name" as "firstName"
	And the user enters "Last Name" as "lastName"
	And the user enters "Email" as "uniqueEmail"
	And the user enters "Password" as "password"
	And the user enters "Confirm Password" as "password"
	And the user clicks the "Create an Account" button
	Then the user should be navigated to "My Account" page
	And the page URL should contain "/customer/account/"

@UI @SignUp @TC009
Scenario: TC009_Sign-Up Page Field Presence
	Then the user should see the following fields on Sign-Up page:
		| First Name        |
		| Last Name         |
		| Email             |
		| Password          |
		| Confirm Password  |
		| Create an Account |
