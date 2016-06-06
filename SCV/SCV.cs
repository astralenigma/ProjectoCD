﻿using BibliotecaDeClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace SCV
{
    class SCV
    {
        static int nmrVotosBrancos;

        public static int NmrVotosBrancos
        {
            get { return SCV.nmrVotosBrancos; }
            set { SCV.nmrVotosBrancos = value; }
        }
        static int votosAprovados;

        public static int VotosAprovados
        {
            get { return SCV.votosAprovados; }
            set { SCV.votosAprovados = value; }
        }
        static int votosReprovados;

        public static int VotosReprovados
        {
            get { return SCV.votosReprovados; }
            set { SCV.votosReprovados = value; }
        }
        private ProcessosComunicacao oPC;
        private Socket serverSocket;
        private String fileName;
        const int PORTASRE = 6000;
        const int PORTATRV = 8888;

        public SCV(String fileName,Chart chart)
        {
            this.fileName = fileName;
            nmrVotosBrancos = 0;
            votosAprovados = 0;
            votosReprovados = 0;
        }

        //Método para estabelecer ligacao ao SRE.
        //private static void estabelecerLigacaoSRE()
        //{
        //    oPC = iniciarPC("127.0.0.1");
        //    Console.WriteLine("Ligação Bem sucedida.\n" +
        //        "Conectado ao Servidor de Recenseamento Eleitoral em " + oPC.getRemoteEndPoint() + ".");
        //}

        //Método porque PC é Rei, PC é Amor, PC é Deus.
        ProcessosComunicacao iniciarPC(string ip)
        {
            try
            {
                return new ProcessosComunicacao(conectar(ip));
            }
            catch (SocketException ex)
            {
                if (ex.ErrorCode == 10061)
                {
                    Console.WriteLine("Conecção ao servidor recusada, pressione qualquer tecla para continuar ou 's' para sair ");
                    if (Console.ReadKey(true).KeyChar.ToString().ToLower() == "s")
                        Environment.Exit(0);
                }
                return iniciarPC(ip);
            }
        }

        //Método de conecção com o SRE
        Socket conectar(String ipStr)
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ipad = IPAddress.Parse(ipStr);
            IPEndPoint ip = new IPEndPoint(ipad, PORTASRE);

            socket.Connect(ip);

            return socket;
        }

        //Método de incrementação de votos de partido
        static void incrementarVoto(int partido)
        {
            if (partido == 1)
            {
                VotosAprovados++;
            }
            else
            {
                VotosReprovados++;
            }

        }

        //Método de incrementação de votos em branco.
        private void incrementarVotoBranco()
        {
            NmrVotosBrancos++;
        }


        //Eu preciso de um TRV, eu estou à espera de um TRV pelo fim da noite.
        public void esperandoPorUmTRV()
        {
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ip = new IPEndPoint(IPAddress.Any, PORTATRV);
            serverSocket.Bind(ip);
            serverSocket.Listen(10);
        }

        //Método que aceita e distribui TRVs por threads.
        public void aceitarTRVs()
        {
            while (true)
            {
                handleTRV client = new handleTRV();//Classe criada só para funcionar com o código do qual não entendo nada, acho piada o facto de já ter alterado o código tanto que já nem deve de fazer a mesma coisa.
                Socket cliSock = serverSocket.Accept();
                ProcessosComunicacao cliPC = new ProcessosComunicacao(cliSock);//Eu fiz esta classe muito mais robusta do que pensava.
                cliPC.enviarMensagem(fileName);
                client.startClient(cliPC);//Loucura de código. Loucura mesmo já me obrigou a trocar de lugares e tudo 2X ou pelo menos é a segunda que me lembro.
                Console.WriteLine("Cliente recebido.");
            }
        }

        //Classe das operações do TRV.
        private class handleTRV
        {
            ProcessosComunicacao cliPC;
            //ProcessosComunicacao srePC;

            public void startClient(ProcessosComunicacao incliPC)
            {
                this.cliPC = incliPC;
                //this.srePC = insrePC;

                Thread ctThread = new Thread(doVoto);
                ctThread.Start();
            }

            private void doVoto()
            {

                //Boolean erro = false;
                try
                {
                    do
                    {
                        String[] mensagem = cliPC.receberMensagem().Split(' ');
                        //ESTÁ TUDO A FUNCIONAR MAS NÃO ME CULPES A MIM.
                        incrementarVoto(Convert.ToInt32(mensagem));
                        cliPC.enviarMensagem("3");
                        //return false;
                        //srePC.enviarMensagem(mensagem[0]);
                        ///*erro =*/ accaoDependeSRE(srePC.receberMensagem(),mensagem[1]);
                    } while (true);

                }
                catch (TRVCaiuException)
                {
                    Console.WriteLine("O cliente desconectou-se.");
                }
                catch (SocketException ex)
                {
                    if (ex.ErrorCode == 10054)
                    {
                        Console.WriteLine("Desconectado do SRE.");
                        cliPC.enviarMensagem("4");
                    }
                    else
                    {
                        Console.WriteLine("Erro na conecção.");
                    }

                }
            }

            //private bool accaoDependeSRE(string respostaSRE,string mensagem)
            //{
            //    try
            //    {
            //        switch (respostaSRE)
            //        {
            //            //Se o BI falhar mandar aviso
            //            case "BI Nao Encontrado"://ESSE BI NÃO EXISTE.

            //                cliPC.enviarMensagem("1");
            //                return true;
            //            //Se o BI já tiver sido usado mandar aviso
            //            case "BI Usado"://ESSE BI JÁ FOI USADO.

            //                cliPC.enviarMensagem("2");
            //                return true;
            //            //Se tudo funcionar mandar que está tudo bem.
            //            default://ESTÁ TUDO A FUNCIONAR MAS NÃO ME CULPES A MIM.
            //                incrementarVoto(Convert.ToInt32(mensagem));
            //                cliPC.enviarMensagem("3");
            //                return false;
            //        }
            //    }
            //    catch (SocketException)
            //    {
            //        throw new TRVCaiuException();
            //    }
            //}

        }
    }
}