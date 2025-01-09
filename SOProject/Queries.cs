using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Microsoft.Win32;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Threading;
using System.Net;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace SOProject
{
    public partial class Queries : Form
    {
        Socket server;
        Thread atender;
        delegate void DelegadoParaPonerTexto(string texto);
        string conectados;
        string myname;
        delegate void hide_window_delegate();
        int acceptedInvitation = 0;
        public string invites;
        public string invited;
        public int idgame;
        NewGame ng;
        ChatRoom room;
        int inv = 0;
        public Queries(Socket s, string conectados, string myname)
        {
            InitializeComponent();
            this.server = s;
            ThreadStart ts = delegate { AtenderServidor(); };
            atender = new Thread(ts);
            atender.Start();
            Console.WriteLine("Hilo iniciado...");
            this.conectados = conectados;
            ConnectedList(conectados);
            this.myname = myname;
        }

        private Dictionary<int, ChatRoom> chatForms = new Dictionary<int, ChatRoom>();
        private void Queries_Load(object sender, EventArgs e)
        {
            ConnectedAs.Text = "Connected as: " + myname;
        }

        private void PlayerGame_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void winner_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void query_Click(object sender, EventArgs e)
        {
            try
            {
                string message = ""; // Prepare the message to send to the server

                if (PlayerGame.Checked)
                {
                    message = "3/";

                }
                else if (winner.Checked)
                {
                    message = "4/" +gameid.Text; // The code for fetching the winner by game ID

                }
                else if (gamesPlayed.Checked)  // New case for Games Played by a Player
                {
                    message = "6/" + playerName.Text; // "6" represents the new query

                }

                else if (mygames.Checked)
                {
                    message = "11/"+myname;

                }
                else if (resultofthegame.Checked)
                { 
                    message = "12/" + myname + "/" + Playername2.Text;
                }
                else if (dateQuery.Checked) // New case for games during selected dates
                {
                    DateTime startDate = monthCalendar1.SelectionStart;
                    DateTime endDate = monthCalendar1.SelectionEnd;

                    // changing format
                    string formattedStartDate = startDate.ToString("yyyy-MM-dd");
                    string formattedEndDate = endDate.ToString("yyyy-MM-dd");

                    message = $"13/{formattedStartDate}/{formattedEndDate}";
                }

                byte[] msg = Encoding.ASCII.GetBytes(message);
                server.Send(msg);
                Console.WriteLine($"Message sent: {message}");
            }

            catch (SocketException ex)
            {
                // If there's a socket exception, show an error message
                MessageBox.Show("Error connecting to the server: " + ex.Message);
            }
        }

            private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void gameid_TextChanged(object sender, EventArgs e)
        {

        }
        private void gamesPlayed_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void playerName_TextChanged(object sender, EventArgs e)
        {

        }

        private void newgame_Click(object sender, EventArgs e)
        {
            if (acceptedInvitation == 1)
            {
                ThreadStart ts = delegate { PonerEnMarchaFormulario(invites,invited,idgame, myname); };
                Thread T = new Thread(ts);
                T.Start();
            }

            else
            {
                MessageBox.Show("You must invite someone and they have to accept your invitation to start a new game.");
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string message = "5/";
            byte[] msg = Encoding.ASCII.GetBytes(message);
            server.Send(msg);   
        }

        public void query1(string message)
        {
            string formattedMessage = message.Replace(",", "\n");
            MessageBox.Show(formattedMessage);
        }

        public void query2(string message)
        {
            MessageBox.Show(message);
        }

        public void query3(string message)
        {
            MessageBox.Show(message);
        }
        public void query4(string [] message)
        {
            string[] trimmedMessage = message.Skip(1).ToArray();
            string combinedMessage = string.Join("/", trimmedMessage);
            string formattedMessage = combinedMessage.Replace("/", "\n");
            MessageBox.Show(formattedMessage);
        }

        public void query5(string [] message)
        {
            Console.WriteLine("Received query 5 response: " + string.Join("/", message)); // Debugging message

            string[] trimmedMessage = message.Skip(1).ToArray();
            string combinedMessage = string.Join("/", trimmedMessage);
            string formattedMessage = combinedMessage.Replace("/", "\n");
            MessageBox.Show(formattedMessage);
        }

        public void query6(string[] message) //List of games played in a given period of time
        {
            Console.WriteLine("Received query 6 response: " + string.Join("/", message)); // Debugging message

            string gamesMessage = string.Join("/", message.Skip(1).ToArray()).Split('\0')[0];
            string formattedMessage = gamesMessage.Replace("/", "\n");
            MessageBox.Show($"Games during the selected dates:\n{formattedMessage}");
        }

        public void ConnectedList(string mensaje)
        {
            Console.WriteLine("ConnectedList ejecutado con: " + mensaje);
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<string>(ConnectedList), mensaje);
            }
            else
            {
                try
                {
                    string[] trozos = mensaje.Split(',');
                    string connected = trozos[0];
                    label_users_connected.Text = connected;

                    dataGridView1.Rows.Clear();
                    for (int i = 1; i < trozos.Length; i++)
                    {
                        dataGridView1.Rows.Add(false, trozos[i]);
                    }
                }
                catch (SocketException ex)
                {
                    MessageBox.Show("Error connecting to the server: " + ex.Message);
                }
            }
        }

        private void Queries_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                // Ensure the server is valid before proceeding
                if (server != null)
                {
                    string mensaje = "0/"+ myname; // Message to notify server for player removal
                    byte[] msg = Encoding.ASCII.GetBytes(mensaje);

                    // Send the message
                    server.Send(msg);
                    atender.Abort();

                    // Gracefully shut down and close the socket
                    server.Shutdown(SocketShutdown.Both);
                    server.Close();                    
                    MessageBox.Show("Disconnected");
                }
            }
            catch (SocketException ex)
            {
                MessageBox.Show($"Socket error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //finally
            //{
            //    // Display the disconnect message, ensuring it always happens
            //    MessageBox.Show("Disconnected");                
            //}
        }

        private void label_users_connected_Click(object sender, EventArgs e)
        {

        }

        public void AtenderServidor() //receive ALL the messages from the server!!
        {
            try
            {
                while (true)
                {
                    //Recibimos mensaje del servidor
                    byte[] msg2 = new byte[1024];
                    Array.Clear(msg2, 0, msg2.Length);
                    server.Receive(msg2);
                    string received = Encoding.ASCII.GetString(msg2);
                    Console.WriteLine("Received: " + received + "\n");
                    string[] trozos = received.Split('/').Select(part => part.Trim()).ToArray();
                    string codigo = (trozos[0]);
                    string mensaje;

                    switch (codigo)
                    {
                        case "1": //query 1
                            string mess = trozos[1].Split('\0')[0];
                            query1(mess);
                            break;

                        case "2": //query 2
                            mensaje = trozos[1].Split('\0')[0];
                            query2(mensaje);
                            break;
                        case "3": //query 3
                            mensaje = trozos[1].Split('\0')[0];
                            query3(mensaje);
                            break;

                        case "4": //Connected List 
                            mensaje = trozos[1].Split('\0')[0];
                            Console.WriteLine("Llamando a ConnectedList...");
                            ConnectedList(mensaje);
                            break;
                        case "7": //invitaion
                            int roomid = Convert.ToInt32(trozos[1]);
                            string pinvites = trozos[2];
                            string message = trozos[3];
                            DialogResult dialogResult = MessageBox.Show(message, "You're about to accept this game", MessageBoxButtons.YesNo);
                            if (dialogResult == DialogResult.Yes)
                            {
                                string accepted = "8/" +roomid+"/"+ pinvites + "/" + "1/"+myname;                                
                                byte[] msg = System.Text.Encoding.ASCII.GetBytes(accepted);
                                server.Send(msg);
                                acceptedInvitation = 1;
                                MessageBox.Show("Wait for the others to be ready, please.");

                            }
                            else //not accepted
                            {
                                string accepted = "8/" + roomid + "/" + pinvites + "/" + "0/" + myname;
                                byte[] msg = System.Text.Encoding.ASCII.GetBytes(accepted);
                                server.Send(msg);
                            }
                            break;

                        case "9": //get id if you are the onw that invites
                            int gameID = Convert.ToInt32(trozos[1]);
                            string player1 = trozos[2];
                            string player2 = trozos[3];

                            if (ng != null)
                            {
                                ng.SetGameID(gameID);
                                ng.UpdatePlayerInfo(player1, player2);
                                ng.ShowPlaceShipsMessage();
                            }

                            break;

                        case "10": // chat
                            string chat = trozos[1];
                            room.UpdateChat(chat);
                            break;
                        case "11":
                            query4(trozos);
                            break;
                        case "12":
                            query5(trozos);
                            break;
                        case "13":
                            query6(trozos);
                            break;
                        case "15":
                            // 15/<chatID>/<inviterName>/message
                            int chatID = int.Parse(trozos[1]);
                            string inviter = trozos[2];
                            string msgg = trozos[3];

                            DialogResult dr = MessageBox.Show($"{inviter} invited you. {msgg}",
                                                              "Chat Invitation",
                                                              MessageBoxButtons.YesNo);
                            int accepte = (dr == DialogResult.Yes) ? 1 : 0;
                            // Now send 16/chatID/myName/accepted
                            string toSend = $"16/{chatID}/{myname}/{accepte}";
                            Console.WriteLine("Sending: " + toSend + "\n");
                            byte[] sendBytes = Encoding.ASCII.GetBytes(toSend);
                            server.Send(sendBytes);
                            break;

                        case "17":
                            // 17/<chatID>/start or cancelled
                            chatID = int.Parse(trozos[1]);
                            string status = trozos[2].Split('\0')[0];
                            if (status == "start")
                            {
                                // Chat is starting
                                Thread t = new Thread(() => OpenChatForm(chatID));                                // create a new partial thread that opens the chat form

                                t.SetApartmentState(ApartmentState.STA);
                                t.Start();
                            }
                            else
                            {
                                // "cancelled"
                                MessageBox.Show($"Chat {chatID} was cancelled");
                            }
                            break;

                        case "19":
                            // 19/<chatID>/<sender>/<message>
                            chatID = int.Parse(trozos[1]);
                            string senderName = trozos[2];
                            string chatMsg = trozos[3];

                            // Find the ChatForm for chatID, if it exists
                            if (chatForms.ContainsKey(chatID))
                            {
                                chatForms[chatID].UpdateChat($"{senderName}: {chatMsg}");
                            }
                            else
                            {
                                // No form? Possibly open or ignore
                            }
                            break;
                    }
                }
            }

            catch (SocketException ex)
            {
                MessageBox.Show($"Socket error (Atender): {ex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unexpected error (Atender): {ex.Message}");
            }
        }

        public void OpenChatForm(int chatID)
        {
            // Must be on the new thread with a message loop if it's a WinForm
            ChatRoom form = new ChatRoom(server, myname, chatID);
            // store it
            chatForms[chatID] = form;
            Application.Run(form);
        }

        private void PonerEnMarchaFormulario(string p1, string p2, int id, string myname)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => PonerEnMarchaFormulario(p1, p2, id,myname)));
            }
            else
            {
                ng = new NewGame(server, p1, p2, id,myname);
                ng.Show();
            }
        }

        private void mygames_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void resultofthegame_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Playername2_TextChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {

        }

        private void invite_Click(object sender, EventArgs e)
        {
            try
            {
                // 1) Collect selected player names
                List<string> selectedNames = new List<string>();

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    // Read the cell that contains the checkbox (column index 0 in your case)
                    object cellValue = row.Cells[0].Value;
                    bool isChecked = (cellValue is bool && (bool)cellValue);

                    if (isChecked)
                    {
                        // The second column (index 1) holds the player's username
                        string playerName = row.Cells[1].Value.ToString().Split('\0')[0];

                        // Make sure we don't invite ourselves
                        if (playerName == myname)
                        {
                            MessageBox.Show("You cannot invite yourself.");
                            continue;
                        }

                        selectedNames.Add(playerName);
                    }
                }

                // 2) Check how many have been selected
                int numInvites = selectedNames.Count;
                if (numInvites == 0)
                {
                    MessageBox.Show("No players selected to invite.");
                    return;
                }

                // 3) Build the message for the server
                //    Suppose you have a new opcode 14 for "multi-chat invite."
                //    Format: "14/myname/numInvites/invitee1/invitee2/..."
                string message = $"14/{myname}/{numInvites}";
                foreach (string name in selectedNames)
                {
                    message += $"/{name}";
                }

                // 4) Send it to the server
                byte[] msgBytes = Encoding.ASCII.GetBytes(message);
                server.Send(msgBytes);

                Console.WriteLine("Invite message sent: " + message);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error processing invitation: {ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConnectedAs_Click(object sender, EventArgs e)
        {

        }
    }
}
