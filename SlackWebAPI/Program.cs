using System;
using static SlackWebAPI.SlackClientAPI;

namespace SlackWebAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            string SlackChannelName = "<INSERT CHANNEL NAME or ID>";
            string SlackToken = "<SLACK BOT TOKEN>";
            string Description = "Cannot open main website";
            string CustomerName = "Simon Crouch";
            string CustomerEmail = "a.b@customer.com";
            string CustomerPhone = "079123456789";
            string CaseNumber = "1234";
            string Url = "https://api.slack.com/docs/message-attachments";
            string System = "Main Website";
            DateTime CreatedOn = new DateTime(2018, 04, 07, 10, 00, 00);
            string Priority = "High";
            string Status = "Open";
            string LastNoteBy = "Stuart";
            DateTime LastNoteAddedOn = new DateTime(2018, 05, 07, 11, 00, 00, DateTimeKind.Local);
            string LastNoteText = "Updated website";

            PostCRMAlert(SlackChannelName, 
                SlackToken,
                CaseNumber,
                Url,
                Description,
                System,
                CreatedOn,
                CustomerName,
                CustomerEmail,
                CustomerPhone,
                Priority,
                Status,
                LastNoteBy,
                LastNoteAddedOn,
                LastNoteText);
        }

        public static void PostCRMAlert(
            string SlackChannelName,
            string SlackToken,
            string CaseNumber,
            string Url,
            string Description,
            string System,
            DateTime CreatedOn,
            string CustomerName,
            string CustomerEmail,
            string CustomerPhone,
            string Priority,
            string Status,
            string LastNoteBy,
            DateTime LastNoteAddedOn,
            string LastNoteText)
        {
            var LastNoteAddedOnEpoch = new DateTimeOffset(LastNoteAddedOn).ToUnixTimeSeconds();

            AttachmentFields caseField = new AttachmentFields()
            {
                Title = "Case Number",
                Value = CaseNumber,
                Short = true
            };
            AttachmentFields systemField = new AttachmentFields()
            {
                Title = "System",
                Value = System,
                Short = true
            };
            AttachmentFields priorityField = new AttachmentFields()
            {
                Title = "Priority",
                Value = Priority,
                Short = true
            };
            AttachmentFields statusField = new AttachmentFields()
            {
                Title = "Status",
                Value = Status,
                Short = true
            };
            Attachment crmAttachment = new Attachment()
            {
                Pretext = $"Priority issue raised for {CustomerName} on {CreatedOn.ToString("dd/MMM/yy")}",
                Fallback = Description,
                Color = "FF0000",
                AuthorName = $"{CustomerName} ({CustomerPhone})",
                AuthorLink = $"mailto:{CustomerEmail}",
                AuthorIcon = "https://cdn4.iconfinder.com/data/icons/48-bubbles/48/29.Mac-256.png",  // bit naughty, direct linking here
                Title = Description,
                TitleLink = Url,
                Fields = { caseField, systemField, priorityField, statusField },
                Footer = $"{LastNoteBy} - {LastNoteText}",
                FooterIcon = "https://cdn4.iconfinder.com/data/icons/cc_mono_icon_set/blacks/48x48/notepad_2.png",  // bit naughty, direct linking here
                TS = LastNoteAddedOnEpoch
            };
            Arguments slackMessage = new Arguments()
            {
                Channel = SlackChannelName,
                Attachments = { crmAttachment }
            };

            // send
            var slack = new SlackClientAPI(SlackToken);
            var response = slack.PostMessage("chat.postMessage", slackMessage);
        }
    }
}
