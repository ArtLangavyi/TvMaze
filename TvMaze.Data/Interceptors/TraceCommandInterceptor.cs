using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Data.Common;
using System.Runtime.CompilerServices;

namespace TvMaze.Data.Interceptors
{
    public class TraceCommandInterceptor : DbCommandInterceptor
    {
        public override void CommandFailed(DbCommand command, CommandErrorEventData eventData)
        {
            LogCommand(command, eventData);

            base.CommandFailed(command, eventData);
        }
        public override Task CommandFailedAsync(DbCommand command, CommandErrorEventData eventData, CancellationToken cancellationToken = default)
        {
            LogCommand(command, eventData);

            return base.CommandFailedAsync(command, eventData, cancellationToken);
        }

        public override int NonQueryExecuted(DbCommand command, CommandExecutedEventData eventData, int result)
        {
            LogCommand(command, eventData);

            return base.NonQueryExecuted(command, eventData, result);
        }


        public override DbDataReader ReaderExecuted(DbCommand command, CommandExecutedEventData eventData, DbDataReader result)
        {
            LogCommand(command, eventData);

            return base.ReaderExecuted(command, eventData, result);
        }


        public override object ScalarExecuted(DbCommand command, CommandExecutedEventData eventData, object result)
        {
            LogCommand(command, eventData);

            return base.ScalarExecuted(command, eventData, result);
        }




        private void LogCommand(DbCommand command, CommandEndEventData eventData, [CallerMemberName] string caller = null)
        {
            if (command?.CommandText == null || eventData == null)
                return;

            var stashForegroundColor = Console.ForegroundColor;
            var stashBackgroundColor = Console.BackgroundColor;

            if (eventData.Duration.TotalMilliseconds < 100)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.BackgroundColor = ConsoleColor.DarkYellow;
            }
            else if (eventData.Duration.TotalMilliseconds < 1000)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.DarkYellow;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.BackgroundColor = ConsoleColor.DarkYellow;
            }

            Console.WriteLine($"({(int)eventData.Duration.TotalMilliseconds}ms) SQL {(caller ?? "query")}: \n '{command.CommandText}' \n\n");

            Console.ForegroundColor = stashForegroundColor;
            Console.BackgroundColor = stashBackgroundColor;
        }
    }
}
