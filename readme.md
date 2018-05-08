# Slack CRM Notifier

Simple Slack notification tool for adding into a Microsoft Dynamics CRM tool as as customer workflow.

The code is designed to simply take in some standard CRM variables from Microsoft Dynamics and post these in a formatted message to Slack.

Due to sandboxing limitations, Newtonsoft JSON library can't be used, so has been coded out of this solution.

This code utilises the Web API of Slack using a Bot token code, as this is the most flexible of all methods and easily allows for better future development.

## Usage

* Create a slack bot Token
* Directly change the code to add this token and a channel ID or `#MyChannel`
* Create a custom workflow in CRM to use the code in this respository