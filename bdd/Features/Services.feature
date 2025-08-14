Feature: EPAM AI Services Navigation

  As a user
  I want to navigate to different AI services on the EPAM website
  So that I can verify the page title and 'Our Related Expertise' section

  @Smoke
  Scenario Outline: Navigate to a specific AI service and verify its contents
    Given I am on the EPAM home page
    When I navigate to the "<Category>" service
    Then the page title should contain "<ExpectedTitle>"
    And the 'Our Related Expertise' section should be visible

    Examples:
      | Category         | ExpectedTitle   |
      | Generative AI    | Generative AI   |
      | Responsible AI   | Responsible AI  |