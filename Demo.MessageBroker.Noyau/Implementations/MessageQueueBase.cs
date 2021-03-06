using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sixeyed.MessageQueue.Messaging
{
    public abstract class MessageQueueBase : IMessageQueue
    {
        protected int _pollingInterval = 100;
        protected bool _isListening;

        public string Address { get; protected set; }

        public bool IsTemporary { get; protected set; }

        public MessagePattern Pattern { get; protected set; }

        public Dictionary<string, object> Properties { get; protected set; }

        protected Direction Direction { get; set; }

        public abstract void InitialiseOutbound(string name, MessagePattern pattern,  bool isTemporary,
                                                Dictionary<string, object> properties = null);

        public abstract void InitialiseInbound(string name, MessagePattern pattern, bool isTemporary,
                                               Dictionary<string, object> properties = null);

        public abstract void Send(Message message);

        public abstract string GetAddress(string name);

        public abstract IMessageQueue GetResponseQueue();

        public abstract IMessageQueue GetReplyQueue(Message message);

        public abstract void Receive(Action<Message> onMessageReceived, bool processAsync, int maximumWaitMilliseconds = 0);

        public virtual void Listen(Action<Message> onMessageReceived, CancellationToken cancellationToken)
        {
            if (_isListening)
                return;

            Task.Factory.StartNew(() => ListenInternal(onMessageReceived, cancellationToken), cancellationToken, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        }

        protected virtual void ListenInternal(Action<Message> onMessageReceived, CancellationToken cancellationToken)
        {
            _isListening = true;
            while (_isListening)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    _isListening = false;
                    cancellationToken.ThrowIfCancellationRequested();
                }
                try
                {
                    Receive(onMessageReceived, true);
                    Thread.Sleep(_pollingInterval);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: {0}", ex);
                }

            }
        }

        public virtual void Receive(Action<Message> onMessageReceived, int maximumWaitMilliseconds = 0)
        {
            Receive(onMessageReceived, false, maximumWaitMilliseconds);
        }

        public virtual void DeleteQueue()
        {
            //do nothing
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected abstract void Dispose(bool disposing);

        protected void Initialise(Direction direction, string name, MessagePattern pattern, bool isTemporary,
                                  Dictionary<string, object> properties = null)
        {
            Direction = direction;
            Pattern = pattern;
            IsTemporary = isTemporary;
            Address = GetAddress(name);
            Properties = properties ?? new Dictionary<string, object>();
        }

        protected void RequireProperty<T>(string name)
        {
            var value = GetPropertyValue<T>(name);
            if (value.Equals(default(T)))
            {
                throw new InvalidOperationException(string.Format("Property named: {0} of type: {1} is required for: {2}", name, typeof(T).Name, Pattern));
            }
        }

        protected T GetPropertyValue<T>(string name)
        {
            T value = default(T);
            if (Properties != null && Properties.Count(x => x.Key == name) == 1 && Properties[name].GetType() == typeof (T))
            {
                value = (T) Properties[name];
            }
            return value;
        }
    }
}
