﻿using Decisions.Jira.Data;
using Decisions.Jira.Data.Project;
using Decisions.Jira.Data.User;
using Decisions.Jira.Steps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decisions.JiraTestSuite
{
    class TestData
    {
        public static JiraCredentials GetJiraCredentials()
        {
            return new JiraCredentials
            {
                User = "ivan@grafsoft.org",
                Password = "OCjzLDJFXlelROp1a1wj0CC0",
                JiraURL = "https://ivankovalchuk.atlassian.net",
                JiraConnection = JiraConnectionType.JiraCloud
            };
        }
        public static JiraCredentials GetServerJiraCredentials()
        {
            return new JiraCredentials
            {
                User = "ivan",
                Password = "password",
                JiraURL = "http://localhost:8080",
                JiraConnection = JiraConnectionType.JiraServer

            };
        }

        public static JiraProjectModel GetJiraProject(JiraCreateUserResponseModel createUserResponse)
        {
            return new JiraProjectModel
            {
                Description = "New Project123",
                Name = "Test123",
                Key = "NP123",
                ProjectIdOrKey = "NP123",
                ProjectTemplateKey = "com.atlassian.jira-core-project-templates:jira-core-project-management",//"com.pyxis.greenhopper.jira:gh-simplified-agility-kanban",
                //LeadAccountId = "5eb2790cb882f90bae55c19b",//"5ea1ce8c1f32260c13047996",
                LeadAccountId = createUserResponse.AccountId,
                Lead= createUserResponse.Name,
                AssigneeType = ProjectLead.UNASSIGNED,
                ProjectTypeKey = "business"

            };
        }

        public static JiraUserModel GetJiraUser()
        {
            string userName = "test" + DateTime.Now.ToString("ddmmyyhhss");
            return new JiraUserModel
            {
                DisplayName = userName + " display",
                EmailAddress = userName + "@domain.com",
                Name = userName,
                Password = userName
            };
        }

        public static JiraAssignProjectModel GetJiraAssignProject(string aProjectIdOrKey, string aUser, long aRoleId)
        {
            return new JiraAssignProjectModel
            {
                /*ProjectIdOrKey = "10000",
                Users = new string[] { "5ea963ee9ce9ee0b8943fed2" },
                RoleId = 10006*/

                /*ProjectIdOrKey = GetJiraProject().ProjectIdOrKey,
                Users = new string[] { "5eb56717a4c57d0b8b2575d9" },
                RoleId = 10029*/
                ProjectIdOrKey = aProjectIdOrKey,
                Users = new string[] { aUser },
                RoleId = aRoleId

            };
        }


        public static JiraIssue GetJiraIssue(string projectId, string jiraIssueId)
        {
            return new JiraIssue
            {
                Details = "New Issue " + DateTime.Now.ToString("ddmmyyhhss"),
                JiraProject = new JiraIdReferenceModel(projectId),
                Issuetype = new JiraIdReferenceModel(jiraIssueId),
                Description = " this is issue description"
            };
        }


    }
}
