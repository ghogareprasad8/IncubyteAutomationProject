Feature: User registration and login

Background: 
Given the user is on the e-commerce website

  Scenario: Create a new user account
    Given the user is on the registration page
    When the user enters valid details and submits the form
    Then the user should be redirected to the My Account page

  Scenario: Login with registered user
    Given the user is on the login page
    When the user enters valid credentials and clicks Sign In
    Then the user should see the My Account page