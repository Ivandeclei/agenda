using System;
using System.Collections.Generic;

namespace Agenda.WebApi.Dtos
{
    public class EventoDto : EventoBaseDto
    {
        /// <summary>
        /// Nome do evento
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Descrição 
        /// </summary>
        public string Descricao { get; set; }

        /// <summary>
        /// Data em que ocorrera o evento
        /// </summary>
        public DateTimeOffset Data { get; set; }

        /// <summary>
        /// Local em que o evento Oorrerá
        /// </summary>
        public string Local { get; set; }

        /// <summary>
        /// Tipo de evento
        /// </summary>
        public TipoEventoDto TipoEvento { get; set; }

    }
}
