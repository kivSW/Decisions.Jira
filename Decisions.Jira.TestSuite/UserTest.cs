﻿using System;
using System.Net;
using Decisions.Jira;
using Decisions.Jira.Data;
using Decisions.Jira.Data.Project;
using Decisions.Jira.Data.User;
using Decisions.Jira.Steps;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;

namespace Decisions.JiraTestSuite
{

    [TestClass]
    public class UserTest
    {
        JiraCredentials CloudCredential { get { return TestData.GetJiraCredentials(); } }
        JiraCredentials ServerCredential { get { return TestData.GetServerJiraCredentials(); } }

        [TestMethod]
        public void CreateUser()
        {
            doCreate(CloudCredential);
            doCreate(ServerCredential);
        }

        private void doCreate(JiraCredentials Credential)
        {
            var newUser = TestData.GetJiraUser();
            JiraCreateUserResult createUserResult = UserSteps.CreateUser(Credential, newUser);
            Assert.AreEqual(createUserResult.Status, JiraResultStatus.Success);

            UserSteps.DeleteUser(Credential, createUserResult.Data.AccountId); // for Jira Cloud
            UserSteps.DeleteUser(Credential, createUserResult.Data.Key); // for Jira server
        }

        [TestMethod]
        public void AssignProject()
        {
            doAssignProject(CloudCredential);
            doAssignProject(ServerCredential);
        }

        private void doAssignProject(JiraCredentials Credential)
        {
            var newUser = TestData.GetJiraUser();
            JiraCreateUserResult createUserResult = UserSteps.CreateUser(Credential, newUser);

            var newProject = TestData.GetJiraProject(createUserResult.Data);
            JiraCreateProjectResult createResponse = ProjectSteps.CreateProject(Credential, newProject);
            JiraProjectRolesResult roles = ProjectSteps.GetProjectRoles(Credential);

            try
            {
                JiraAssignProjectModel assignProject;
                if (Credential.JiraConnection == JiraConnectionType.JiraCloud)
                    assignProject = TestData.GetJiraAssignProject(newProject.Key, createUserResult.Data.AccountId, roles.Data[0].Id);
                else
                    assignProject = TestData.GetJiraAssignProject(newProject.Key, createUserResult.Data.Key, roles.Data[0].Id);

                JiraAssignProjectResult assignProjectResult = UserSteps.AssignProject(Credential, assignProject);
                Assert.AreEqual(assignProjectResult.Status, JiraResultStatus.Success);
            }
            finally
            {
                try
                {
                    ProjectSteps.DeleteProject(Credential, newProject.ProjectIdOrKey);
                    UserSteps.DeleteUser(Credential, createUserResult.Data.Key); // for Jira server
                    UserSteps.DeleteUser(Credential, createUserResult.Data.AccountId); // for Jira cloud
                }
                catch (Exception ex) { _ = ex.Message; }
            }

        }

        [TestMethod]
        public void DeleteUser()
        {
            doDeleteUser(CloudCredential);
            doDeleteUser(ServerCredential);
        }

        private void doDeleteUser(JiraCredentials Credential)
        {

            var newUser = TestData.GetJiraUser();
            JiraCreateUserResult createUserResult = UserSteps.CreateUser(Credential, newUser);

            BaseJiraResult deleteResult;
            if (Credential.JiraConnection == JiraConnectionType.JiraCloud)
                deleteResult=UserSteps.DeleteUser(TestData.GetJiraCredentials(), createUserResult.Data.AccountId);
            else
                deleteResult=UserSteps.DeleteUser(Credential, createUserResult.Data.Key);

            Assert.AreEqual(deleteResult.Status, JiraResultStatus.Success);

        }

        [TestMethod]
        public void EditUser()
        {
            //doEditUser(CloudCredential);
            doEditUser(ServerCredential);
        }

        private void doEditUser(JiraCredentials Credential)
        {
            var editedUser = TestData.GetJiraUser();
            JiraCreateUserResult createUserResult = UserSteps.CreateUser(Credential, editedUser);

            JiraUserModel newUserData = new JiraUserModel
            {
                DisplayName = "edited" + editedUser.DisplayName,
                EmailAddress = "edited" + editedUser.EmailAddress,
                Name = "edited" + editedUser.Name,
                Password = null
            };

            try
            {
                var EditUserResult = UserSteps.EditUser(Credential, createUserResult.Data.Key, newUserData);
                Assert.AreEqual(EditUserResult.Status, JiraResultStatus.Success);
            }
            finally
            {
                try
                {
                    UserSteps.DeleteUser(Credential, createUserResult.Data.Key); // for Jira server
                    UserSteps.DeleteUser(Credential, createUserResult.Data.AccountId); // for Jira cloud
                }
                catch (Exception ex) { _ = ex.Message; }
            }
        }

        [TestMethod]
        public void SetUserPassword()
        {
            //doSetUserPassword(CloudCredential);
            doSetUserPassword(ServerCredential);
        }
        private void doSetUserPassword(JiraCredentials Credential)
        {
            var editedUser = TestData.GetJiraUser();
            JiraCreateUserResult createUserResult = UserSteps.CreateUser(Credential, editedUser);

            try
            {
                var serPasswordResult = UserSteps.SetUserPassword(Credential, createUserResult.Data.Key, "123");
                Assert.AreEqual(serPasswordResult.Status, JiraResultStatus.Success);
            }
            finally
            {
                try
                {
                    UserSteps.DeleteUser(Credential, createUserResult.Data.Key); // for Jira server
                    UserSteps.DeleteUser(Credential, createUserResult.Data.AccountId); // for Jira cloud
                }
                catch (Exception ex) { _ = ex.Message; }
            }
        }


    }



}
