using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace TheMinx
{
    public class MessageViewerInspector : IEndpointBehavior, IClientMessageInspector
    {
        #region Properties

        public string RequestMessage { get; set; }
        public string ResponseMessage { get; set; }

        #endregion

        #region

        IEndpointBehavior Members;

        public void AddBindingParameters(ServiceEndpoint endpoint, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {

        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            // adds our inspector to the runtime
            clientRuntime.MessageInspectors.Add(this);
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {

        }

        public void Validate(ServiceEndpoint endpoint)
        {

        }

        #endregion Properties


        #region

        IClientMessageInspector Members1;

        void IClientMessageInspector.AfterReceiveReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
        {
            this.ResponseMessage = reply.ToString();
            Console.WriteLine(this.ResponseMessage);
        }

        object IClientMessageInspector.BeforeSendRequest(ref System.ServiceModel.Channels.Message request, IClientChannel channel)
        {
            this.RequestMessage = request.ToString();
            Console.WriteLine(this.RequestMessage);
            return null;
        }

        #endregion
    }
}

