using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaDoZe.Presentation.AppMauii.Message
{
	public sealed class TemaPreferencesUpdatedMessage(string value) : ValueChangedMessage<string>(value)
	{
	}
}
