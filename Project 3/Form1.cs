using System;
using System.IO; // Required for OS file operations
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace CS3502_P3_FileSystem_JacksonDeal
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            // Start at the user's Documents folder
            string startPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            txtPath.Text = startPath;
            LoadDirectory(startPath);

            // Wire up the Go button event
            btnGo.Click += (s, e) => LoadDirectory(txtPath.Text);
        }

        private void LoadDirectory(string path)
        {
            try
            {
                if (!Directory.Exists(path))
                {
                    statusLabel.Text = "● Error: Path not found (ENOENT)";
                    return;
                }

                lstContents.Items.Clear();

                // Get Directories and Files (OS calls: opendir, readdir)
                foreach (var dir in Directory.GetDirectories(path))
                    lstContents.Items.Add("[DIR] " + Path.GetFileName(dir));

                foreach (var file in Directory.GetFiles(path))
                    lstContents.Items.Add("[FILE] " + Path.GetFileName(file));

                statusLabel.Text = "● Current Status: Ready";
                statusItems.Text = $"{lstContents.Items.Count} Items";
            }
            catch (UnauthorizedAccessException)
            {
                statusLabel.Text = "● Error: Permission Denied (EACCES)";
            }
            catch (Exception ex)
            {
                statusLabel.Text = $"● Error: {ex.Message}";
            }
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            if (lstContents.SelectedItem == null)
            {
                statusLabel.Text = "● Please select a file or directory.";
                return;
            }

            string selectedItem = lstContents.SelectedItem.ToString();
            bool isDirectory = selectedItem.StartsWith("[DIR]");
            string cleanName = selectedItem.Replace("[DIR] ", "").Replace("[FILE] ", "");
            string fullPath = Path.Combine(txtPath.Text, cleanName);

            try
            {
                if (isDirectory)
                {
                    // If it's a directory, update path and navigate
                    txtPath.Text = fullPath;
                    LoadDirectory(fullPath);
                    statusLabel.Text = "● Navigated into: " + cleanName;
                }
                else
                {
                    // If it's a file, read the contents
                    txtFileContent.Text = File.ReadAllText(fullPath);
                    txtFileContent.ReadOnly = false; // Allow editing after reading
                    statusLabel.Text = $"● Read: {cleanName}";
                }
            }
            catch (UnauthorizedAccessException)
            {
                statusLabel.Text = "● Error: Permission Denied (EACCES)";
            }
            catch (Exception ex)
            {
                statusLabel.Text = $"● Read Error: {ex.Message}";
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            // Toggle: Ask the user if they want to create a File or a Directory
            DialogResult typeChoice = MessageBox.Show("Create a new Directory? (Select 'No' for a File)",
                "Select Type", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            if (typeChoice == DialogResult.Cancel) return;

            bool isCreatingDirectory = (typeChoice == DialogResult.Yes);
            string promptTitle = isCreatingDirectory ? "Create Directory" : "Create File";
            string defaultName = isCreatingDirectory ? "NewFolder" : "newfile.txt";

            // Prompt for the name
            string itemName = Microsoft.VisualBasic.Interaction.InputBox($"Enter new {(isCreatingDirectory ? "directory" : "file")} name:",
                promptTitle, defaultName);

            if (string.IsNullOrWhiteSpace(itemName)) return;

            string fullPath = Path.Combine(txtPath.Text, itemName);

            try
            {
                if (isCreatingDirectory)
                {
                    if (Directory.Exists(fullPath))
                    {
                        statusLabel.Text = "● Error: Directory already exists (EEXIST)";
                        return;
                    }
                    Directory.CreateDirectory(fullPath);
                    statusLabel.Text = $"● Directory Created: {itemName}";
                }
                else
                {
                    if (File.Exists(fullPath))
                    {
                        statusLabel.Text = "● Error: File already exists (EEXIST)";
                        return;
                    }
                    using (FileStream fs = File.Create(fullPath)) { }
                    statusLabel.Text = $"● File Created: {itemName}";
                }

                LoadDirectory(txtPath.Text); // Refresh the list
            }
            catch (UnauthorizedAccessException)
            {
                statusLabel.Text = "● Error: Permission Denied (EACCES)";
            }
            catch (Exception ex)
            {
                statusLabel.Text = $"● Create Error: {ex.Message}";
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lstContents.SelectedItem == null)
            {
                statusLabel.Text = "● Please select an item to delete.";
                return;
            }

            string selectedItem = lstContents.SelectedItem.ToString();
            bool isDirectory = selectedItem.StartsWith("[DIR]");
            string itemName = selectedItem.Replace("[DIR] ", "").Replace("[FILE] ", "");
            string fullPath = Path.Combine(txtPath.Text, itemName);

            DialogResult confirm = MessageBox.Show($"Are you sure you want to delete {itemName}?",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes) return;

            try
            {
                if (isDirectory)
                {
                    Directory.Delete(fullPath, true);
                }
                else
                {
                    File.Delete(fullPath); // OS call: unlink
                }

                statusLabel.Text = $"● Deleted: {itemName}";
                LoadDirectory(txtPath.Text); // Refresh UI
            }
            catch (UnauthorizedAccessException)
            {
                statusLabel.Text = "● Error: Permission Denied (EACCES)";
            }
            catch (IOException)
            {
                statusLabel.Text = "● Error: Item in use or directory not empty (EBUSY)";
            }
            catch (Exception ex)
            {
                statusLabel.Text = $"● Delete Error: {ex.Message}";
            }
        }

        private void btnRename_Click(object sender, EventArgs e)
        {
            if (lstContents.SelectedItem == null)
            {
                statusLabel.Text = "● Please select an item to rename.";
                return;
            }

            string selectedItem = lstContents.SelectedItem.ToString();
            string oldName = selectedItem.Replace("[DIR] ", "").Replace("[FILE] ", "");
            string oldPath = Path.Combine(txtPath.Text, oldName);

            string newName = Microsoft.VisualBasic.Interaction.InputBox($"Enter new name for {oldName}:", "Rename", oldName);

            if (string.IsNullOrWhiteSpace(newName) || newName == oldName) return;

            string newPath = Path.Combine(txtPath.Text, newName);

            try
            {
                if (selectedItem.StartsWith("[DIR]"))
                {
                    Directory.Move(oldPath, newPath);
                }
                else
                {
                    File.Move(oldPath, newPath); // Atomic operation
                }

                statusLabel.Text = $"● Renamed: {oldName} -> {newName}";
                LoadDirectory(txtPath.Text);
            }
            catch (IOException ex) when (ex.Message.Contains("already exists"))
            {
                statusLabel.Text = "● Error: Target name already exists (EEXIST)";
            }
            catch (UnauthorizedAccessException)
            {
                statusLabel.Text = "● Error: Permission Denied (EACCES)";
            }
            catch (Exception ex)
            {
                statusLabel.Text = $"● Rename Error: {ex.Message}";
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (lstContents.SelectedItem == null || !lstContents.SelectedItem.ToString().StartsWith("[FILE]"))
            {
                statusLabel.Text = "● Error: Select a file to save changes.";
                return;
            }

            string fileName = lstContents.SelectedItem.ToString().Replace("[FILE] ", "");
            string fullPath = Path.Combine(txtPath.Text, fileName);

            try
            {
                File.WriteAllText(fullPath, txtFileContent.Text);
                statusLabel.Text = $"● Updated: {fileName} successfully.";

                btnUpdate.Enabled = false;
                Timer t = new Timer { Interval = 1000 };
                t.Tick += (s, ev) => { btnUpdate.Enabled = true; t.Stop(); };
                t.Start();
            }
            catch (UnauthorizedAccessException)
            {
                statusLabel.Text = "● Error: Access Denied (EACCES) - File might be Read-Only.";
            }
            catch (IOException)
            {
                statusLabel.Text = "● Error: Disk full or file locked (EIO/EBUSY).";
            }
            catch (Exception ex)
            {
                statusLabel.Text = $"● Update Error: {ex.Message}";
            }
        }

        private void lstContents_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // Re-using the btnRead click logic for double-click consistency
            btnRead_Click(sender, e);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            DirectoryInfo parentDir = Directory.GetParent(txtPath.Text);

            if (parentDir != null)
            {
                txtPath.Text = parentDir.FullName;
                LoadDirectory(parentDir.FullName);
                statusLabel.Text = "● Moved up to: " + parentDir.Name;
            }
            else
            {
                statusLabel.Text = "● Error: Already at the root directory.";
            }
        }
    }
}