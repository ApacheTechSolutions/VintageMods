﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using VintageMods.Core.Common.Reflection;
using VintageMods.Core.FluentChat.Attributes;
using Vintagestory.API.Common;
using Vintagestory.API.Config;
using Vintagestory.Client.NoObf;

// ReSharper disable MemberCanBeProtected.Global

namespace VintageMods.Core.FluentChat.Primitives
{
    public abstract class ChatCommandBase<TApi> where TApi : ICoreAPI
    {
        protected TApi Api { get; }

        /// <summary>
        ///     Initialises a new instance of the <see cref="ChatCommandBase{TApi}"/> class.
        /// </summary>
        /// <param name="api">The sided core app api, used to call game mechanics.</param>
        protected ChatCommandBase(TApi api)
        {
            Api = api;
            Options = new Dictionary<string, MethodInfo>();
        }

        internal Dictionary<string, MethodInfo> Options { get; }

        internal void CallHandler(int groupId, CmdArgs cmdArgs)
        {
            try
            {
                var option = cmdArgs.PeekWord();
                if (string.IsNullOrWhiteSpace(option))
                {
                    OnNoOption(string.Empty, cmdArgs);
                }
                else if (!Options.ContainsKey(option))
                {
                    option = cmdArgs.PopWord();
                    OnCustomOption(option, cmdArgs);
                }
                else
                {
                    option = cmdArgs.PopWord();
                    OnKnownOption(Options[option], option, cmdArgs);
                }
            }
            catch (Exception ex)
            {
                Api.Logger.Audit(ex.Message);
                Api.Logger.Error(ex.Message);
                Api.Logger.Error(ex.StackTrace);
            }
        }

        /// <summary>
        ///     Called when the given option is not found within the FluentOption dictionary.
        /// </summary>
        /// <param name="option">The option passed to the command.</param>
        /// <param name="args">The remaining arguments.</param>
        public virtual void OnCustomOption(string option, CmdArgs args)
        {
            ((ClientMain)Api.World).ShowChatMessage(HelpText());
        }

        /// <summary>
        ///     Called when no arguments are passed to the command.
        ///     By default, this shows the help text for the command. This behaviour can be overridden.
        /// </summary>
        /// <param name="option">The option. This should be an empty string.</param>
        /// <param name="args">The arguments. This should be an empty list.</param>
        public virtual void OnNoOption(string option, CmdArgs args)
        {
            ((ClientMain)Api.World).ShowChatMessage(HelpText());
        }

        /// <summary>
        ///     Called when the given option is found within the FluentOption dictionary.
        /// </summary>
        /// <param name="method">The method name to call.</param>
        /// <param name="option">The option passed to the command.</param>
        /// <param name="args">The remaining arguments.</param>
        public virtual void OnKnownOption(MethodInfo method, string option, CmdArgs args)
        {
            this.CallMethod(method.Name, option, args);
        }

        /// <summary>
        ///     The default help text to display when using the .help command.
        /// </summary>
        /// <returns>By default, returns the description of the command, as set within the language files. This can be overridden.</returns>
        public virtual string HelpText()
        {
            var cmdAttribute = GetType().GetCustomAttributes().OfType<ChatCommandAttribute>().FirstOrDefault();
            if (cmdAttribute == null || string.IsNullOrEmpty(cmdAttribute.Name)) return "";
            return Lang.Get(cmdAttribute.Description);
        }
    }
}
