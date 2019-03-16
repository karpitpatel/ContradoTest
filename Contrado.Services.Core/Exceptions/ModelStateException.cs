using Contrado.Services.Core.Models.Error;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace Contrado.Services.Core.Exceptions
{
    /// <summary>
    /// Model State Exception
    /// </summary>
    [Serializable]
    public sealed class ModelStateException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModelStateException"/> class
        /// </summary>
        public ModelStateException() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModelStateException"/> class
        /// </summary>
        /// <param name="message">The message that describes the error</param>
        public ModelStateException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModelStateException"/> class
        /// </summary>
        /// <param name="message">The message that describes the error</param>
        /// <param name="innerException">
        /// The exception that is the cause of the current exception, or a null reference
        /// if no inner exception is specified
        /// </param>
        public ModelStateException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModelStateException"/> class
        /// </summary>
        /// <param name="info">
        /// The <see cref="SerializationInfo"/> that holds the serialized object data
        /// about the exception being thrown
        /// </param>
        /// <param name="context">
        /// The <see cref="StreamingContext"/> hat contains contextual information
        /// about the source or destination
        /// </param>
        public ModelStateException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }

        /// <summary>
        /// Gets or sets the model-state error
        /// </summary>
        public ModelStateErrorModel ModelStateError { get; set; }

        /// <summary>
        /// Gets object data for serialization
        /// </summary>
        /// <param name="info">
        /// The <see cref="SerializationInfo"/> that holds the serialized object data
        /// about the exception being thrown
        /// </param>
        /// <param name="context">
        /// The <see cref="StreamingContext"/> hat contains contextual information
        /// about the source or destination
        /// </param>
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null) throw new ArgumentNullException("info");

            if (this.ModelStateError != null && !string.IsNullOrEmpty(this.ModelStateError.Message))
            {
                info.AddValue("ModelStateErrorModel", this.ModelStateError.Message);
            }

            base.GetObjectData(info, context);
        }
    }
}
