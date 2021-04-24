﻿using System;

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
// ReSharper disable MemberCanBePrivate.Global

namespace VintageMods.Core.FluentChat.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public sealed class FluentChatCommandAttribute : Attribute
    {
        public FluentChatCommandAttribute(string name, string description, string syntaxMessage)
        {
            (Name, Description, SyntaxMessage) = (name, description, syntaxMessage);
        }


        public FluentChatCommandAttribute(string name, string description)
        {
            (Name, Description) = (name, description);
        }


        public FluentChatCommandAttribute(string name)
        {
            Name = name;
        }

        public FluentChatCommandAttribute()
        {
        }

        public string Name { get; set; }

        public string Description { get; set; } = "";

        public string SyntaxMessage { get; set; } = "";
    }
}