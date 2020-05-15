﻿using System;
using System.Runtime.Serialization;
using DecisionsFramework.Design.Properties;
using DecisionsFramework.Design.Properties.Attributes;
using Newtonsoft.Json;

namespace Decisions.Jira.Data
{

    public enum ProjectLead { PROJECT_LEAD, UNASSIGNED };

    [DataContract]
    public class JiraProjectModel
    {
        [DataMember]
        [JsonIgnore]
        [PropertyClassificationAttribute("ProjectId or Key", 1)]
        public string ProjectIdOrKey { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "name")]
        [PropertyClassificationAttribute("Name", 2)]
        public string Name { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "key")]
        [PropertyClassificationAttribute("Key", 3)]
        public string Key { get; set; }

        [DataMember]
        [JsonIgnore]
        [JsonProperty(PropertyName = "projectTypeKey")]
        [SelectStringEditorAttribute( new string[]{"software", "service_desk", "business"} )]
        [PropertyClassificationAttribute("Project Type Key",4)]
        public string ProjectTypeKey { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "permissionScheme")]
        [PropertyClassificationAttribute("Permission Scheme",5)]
        public long? PermissionScheme { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "projectTemplateKey")]
        [SelectStringEditorAttribute(new string[] {"com.pyxis.greenhopper.jira:gh-simplified-agility-kanban",
                        "com.pyxis.greenhopper.jira:gh-simplified-agility-scrum",
                        "com.pyxis.greenhopper.jira:gh-simplified-basic",
                        "com.pyxis.greenhopper.jira:gh-simplified-kanban-classic",
                        "com.pyxis.greenhopper.jira:gh-simplified-scrum-classic",
                        "com.atlassian.servicedesk:simplified-it-service-desk",
                        "com.atlassian.servicedesk:simplified-internal-service-desk",
                        "com.atlassian.servicedesk:simplified-external-service-desk",
                        "com.atlassian.servicedesk:simplified-hr-service-desk",
                        "com.atlassian.servicedesk:simplified-facilities-service-desk",
                        "com.atlassian.jira-core-project-templates:jira-core-simplified-content-management",
                        "com.atlassian.jira-core-project-templates:jira-core-simplified-document-approval",
                        "com.atlassian.jira-core-project-templates:jira-core-simplified-lead-tracking",
                        "com.atlassian.jira-core-project-templates:jira-core-simplified-process-control",
                        "com.atlassian.jira-core-project-templates:jira-core-simplified-procurement",
                        "com.atlassian.jira-core-project-templates:jira-core-simplified-project-management",
                        "com.atlassian.jira-core-project-templates:jira-core-simplified-recruitment",
                        "com.atlassian.jira-core-project-templates:jira-core-simplified-task-tracking",
                        "com.atlassian.jira.jira-incident-management-plugin:im-incident-management"
            })]
        [PropertyClassificationAttribute("Project Template Key",6)]
        public string ProjectTemplateKey { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "description")]
        [RichTextInputEditorAttribute]
        [PropertyClassificationAttribute("Description", 7)]
        public string Description { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "notificationScheme")]
        [PropertyClassificationAttribute("Notification Scheme",8)]
        public long? NotificationScheme { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "leadAccountId")]
        [PropertyClassificationAttribute("Lead Account Id",9)]
        public string LeadAccountId { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "assigneeType")]
        [PropertyClassificationAttribute("Assignee Type",10)]
        public ProjectLead AssigneeType { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "categoryId")]
        [PropertyClassificationAttribute("Category Id",11)]
        public long? CategoryId { get; set; }

    }
}
