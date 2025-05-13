using System;
using System.Collections.Generic;
using System.Security.Policy;
using System.Threading;

namespace Programming_2_Midterms
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //---------------------------------------------------------------------------------------

            // Important Variables

            // -----arrays used-----

            string[] studentNames = { "Yuan", "Eudrick", "Chris", "Iram", "Justin", "Ethan", "John" }; // fixed student names

            string[] librarianNames = { "Julian", "Liza", "Mark", "Gilbert", "Rose" }; // fixed librarian names

            // -----dictionaries used-----

            Dictionary<string, string> BooksAndAuthors = new Dictionary<string, string>(); // Dictionary to store fixed book titles and authors


            BooksAndAuthors.Add("The Elements of Moral Philosophy", "James Rachels");
            BooksAndAuthors.Add("Practical Ethics", "Peter Singer");
            BooksAndAuthors.Add("Gödel, Escher, Bach: An Eternal Golden Braid", "Douglas Hofstadter");
            BooksAndAuthors.Add("Clean Code: A Handbook of Agile Software Craftsmanship", "Robert C. Martin");
            BooksAndAuthors.Add("The Pragmatic Programmer", "Andrew Hunt and David Thomas");

            Dictionary<string, string> pendingBookRequests = new Dictionary<string, string>(); // Dictionary to store pending book requests (book title and student name)

            Dictionary<string, string> borrowedBooksTracker = new Dictionary<string, string>(); // Dictionary to track borrowed books (book title and student name)

            Dictionary<string, List<string>> studentBorrowedBooks = new Dictionary<string, List<string>>(); // Dictionary to track borrowed books by each student (student name and list of book titles)


            // -----lists used-----

            List<string> bookTitles = new List<string>(BooksAndAuthors.Keys); // List of book titles

            List<string> availableBooks = new List<string>(); // List of available books

            List<string> borrowedBooks = new List<string>(); // List of borrowed books - ONLY CONTAIN APPROVED BOOKS

            List<string> approvedBooks = new List<string>(); // List of approved books

            List<string> studentBorrowRequests = new List<string>(); // List of student borrow requests

            List<string> requestStatuses = new List<string>(); // List of request statuses

            List<string> requestStudents = new List<string>(); // List of students who made requests

            List<string> returnedBooks = new List<string>(); // List of returned books

            List<string> pendingBooks = new List<string>(); // List of pending books

            List<string> pendingUsers = new List<string>(); // List of users with pending requests

            List<int> pendingIndices = new List<int>(); // List of indices for pending requests

            // -----logging in variables-----

            string userName; // User's name

            string userInput; // User input for role selection ( implicit casted to int role variable)

            int role; //  New stored userInput (User's role (1 for Student, 2 for Librarian))

            bool exitProgram; // Flag to check if user is logged in

            string userChoice; // User's choice in the menu

            bool validUser; // Flag to check if user is valid

            bool keepRunning; // Flag to keep the program running

            // -----case 1 variables-----

            string newTitle; // New book title

            string newAuthor; // New book author

            int displayIndex; // Index for displaying available books (for students)

            bool currentUserPending; // Flag to check if the current user has a pending request for the book

            bool currentUserDeclined; // Flag to check if the current user has a declined request for the book

            bool anotherUserPending; // Flag to check if another user has a pending request for the book


            // -----case 2 variables-----

            string userInput2; // implicit casting to booktoborrow variable

            int bookToBorrow; // Book title to borrow ( by numbers)

            string titleHolder; // Book title for displaying borrowed books

            string selectedBook; // Book title for displaying borrowed books

            string borrower; // Borrower name for displaying borrowed books (for librarians)

            string enteredName; // user Input on entering name

            bool anotherUserHasPending; // Flag to check if another user has a pending request for the book

            string pendingUser; // User with pending request for the book

            // -----case 3 variables-----

            string bookTitle; // Book title for displaying borrowed books (for students)

            bool hasPending; // flsg to check if there are pending requests

            bool hasDeclinedBooks; // flag to check if there are declined requests

            bool hasPendingBooks; // flag to check if there are pending requests

            int existingRequestIndex; // Index of the existing request in the list

            bool hasAnyBooks; // flag to check if there are any books borrowed

            bool isAlreadyBorrowed; // flag to check if the book is already borrowed

            int requestIdx; // Index of the request in the list

            bool hasApprovedBooks; // flag to check if there are approved books

            // -----case 4 variables-----

            int requestIndex; // Index of the request in the list

            int c; // Loop variable for iterating through lists

            string approvalInput; // user Input if approved or not

            int bookIndex; // Book index for returning a book

            string ReturnNow; // Book title to return

            string title; // Book title for displaying borrowed books (for students)

            string userInput3; // implicit casting to bookIndex variable

            string requester; // requester name

            string currentStatus; // Book status for the request

            string ADinput; // user input for approve/decline request

            int selection; // user input for approve/decline request

            //---------------------------------------------------------------------------------------

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine();

            // Raw Graphical User Interface Design

            Console.SetCursorPosition(35, 8);
            Console.WriteLine("SISC ");
            Console.SetCursorPosition(80, 8);

            Console.WriteLine();
            Console.WriteLine("                                   ___      ___   _______  ______    _______  ______    __   __ ");
            Console.WriteLine("                                  |   |    |   | |  _    ||    _ |  |   _   ||    _ |  |  | |  |");
            Console.WriteLine("                                  |   |    |   | | |_|   ||   | ||  |  |_|  ||   | ||  |  |_|  |");
            Console.WriteLine("                                  |   |    |   | |       ||   |_||_ |       ||   |_||_ |       |");
            Console.WriteLine("                                  |   |___ |   | |  _   | |    __  ||       ||    __  ||_     _|");
            Console.WriteLine("                                  |       ||   | | |_|   ||   |  | ||   _   ||   |  | |  |   |  ");
            Console.WriteLine("                                  |_______||___| |_______||___|  |_||__| |__||___|  |_|  |___|  ");
            Console.ResetColor();

            Console.SetCursorPosition(44, 25);
            Console.WriteLine("     Press any key to continue...");
            Console.ReadKey();

            Console.SetCursorPosition(49, 28);
            Console.Write("  Loading"); // loading animation

            for (int i = 0; i < 3; i++)
            {
                Console.Write(".");
                Thread.Sleep(300);
            }

            Console.Clear();

            exitProgram = false;

            // Main login loop
            do
            {
                // log in interface
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("\nPlease Login");
                Console.Write("\n=================");
                Console.ResetColor();
                Console.WriteLine();
                Console.Write("\nEnter your name: ");

                userName = Console.ReadLine().Trim().ToUpper();

                Console.Write("\nEnter your role (1 for Student, 2 for Librarian): ");

                userInput = Console.ReadLine();

                if (!int.TryParse(userInput, out role) || role < 1 || role > 2) // if user inputs letters/special characters or chooses none between 1 and 2
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nThe input must be a number between 1 and 2 only. Try again.");
                    Console.ResetColor();
                    Console.ReadKey();
                    continue;
                }

                validUser = false;

                // idiot-proofing on logging in and case insensitivity function
                if (role == 1) // student
                {
                    foreach (var student in studentNames) // loop through the student names
                    {
                        if (userName == student.ToUpper())
                        {
                            validUser = true;
                            break;
                        }
                    }
                }
                else if (role == 2) // librarian
                {
                    foreach (var librarian in librarianNames) // loop through the librarian names
                    {
                        if (userName == librarian.ToUpper())
                        {
                            validUser = true;
                            break;
                        }
                    }
                }

                if (!validUser) // idiot-proof if user has invalid name or role
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nInvalid name or role. Try again.");
                    Console.ResetColor();
                    Console.WriteLine("\nPress any key to return to login...");
                    Console.ReadKey();
                    continue;
                }

                Console.Clear();
                Console.Write("\nLoading Please Wait");

                for (int j = 0; j < 5; j++)
                {
                    Console.Write(".");
                    Thread.Sleep(300);
                }
                Console.Clear();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nWelcome {userName}! You are logged in as {(role == 1 ? "'Student'" : "'Librarian'")}."); // looged in succesfully
                Console.ResetColor();
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();

                keepRunning = true;

                while (keepRunning) // loop for the menu options
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nMenu:");

                    if (role == 1) // menu option for student
                    {
                        Console.WriteLine("\n1. View Available Books");
                        Console.WriteLine("\n2. Borrow Book");
                        Console.WriteLine("\n3. View Borrowed Books");
                        Console.WriteLine("\n4. Return Book");
                        Console.WriteLine("\n5. Logout");

                    }
                    else // menu option for librarian
                    {
                        Console.WriteLine("\n1. Add New Book");
                        Console.WriteLine("\n2. View All Books");
                        Console.WriteLine("\n3. View Pending Requests");
                        Console.WriteLine("\n4. Approve/Decline Requests");
                        Console.WriteLine("\n5. Logout");

                    }

                    Console.Write("\nChoose an option: ");
                    userChoice = Console.ReadLine();

                    switch (userChoice) // switch case for menu options
                    {
                        case "1": // case handles viewing books (students) or adding new books (librarians)

                            if (role == 1) // student
                            {
                                Console.Clear();

                                Console.WriteLine("\nAvailable Books:");

                                availableBooks.Clear(); // Clear the available books list for each new request




                                displayIndex = 1; // Initialize the display index to start from 1

                                for (int i = 0; i < bookTitles.Count; i++) // Loop through the book titles
                                {
                                    titleHolder = bookTitles[i];

                                    // Clean up any lingering declined/returned data
                                    pendingBookRequests.Remove(titleHolder);
                                    borrowedBooks.Remove(titleHolder);

                                    if (!approvedBooks.Contains(titleHolder))
                                    {
                                        // Only add to availableBooks once, if it hasn't already been added
                                        if (!availableBooks.Contains(titleHolder))
                                        {
                                            availableBooks.Add(titleHolder);
                                        }

                                        // Use the displayIndex instead of availableBooks.Count + 1
                                        Console.ForegroundColor = ConsoleColor.Yellow;
                                        Console.Write($"\n{displayIndex}. {titleHolder}");
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        Console.Write(" by " + BooksAndAuthors[titleHolder]);


                                        // Increment displayIndex for the next book
                                        displayIndex++;

                                        currentUserPending = false;
                                        currentUserDeclined = false;
                                        anotherUserPending = false;

                                        for (int r = 0; r < studentBorrowRequests.Count; r++) // loop through the student borrow requests
                                        {
                                            if (studentBorrowRequests[r] == titleHolder) // check if the book title matches
                                            {
                                                if (requestStudents[r] == userName && requestStatuses[r] == "Pending") // check if the current user has a pending request
                                                    currentUserPending = true;

                                                else if (requestStudents[r] == userName && requestStatuses[r] == "Declined") // check if the current user has a declined request
                                                    currentUserDeclined = true;

                                                else if (requestStudents[r] != userName && requestStatuses[r] == "Pending") // check if another user has a pending request
                                                    anotherUserPending = true;
                                            }
                                        }

                                        if (currentUserPending) // if the current user has a pending request
                                        {
                                            Console.ForegroundColor = ConsoleColor.DarkGray;
                                            Console.Write(" (Request Pending)");
                                        }
                                        else if (currentUserDeclined) // if the current user has a declined request
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.Write(" (Request Declined)");
                                        }
                                        else if (anotherUserPending) // if another user has a pending request
                                        {
                                            Console.ForegroundColor = ConsoleColor.DarkGray;
                                            Console.Write(" (Requested by another user - Pending)");
                                        }
                                        else // if the book is available
                                        {
                                            Console.ForegroundColor = ConsoleColor.Green;
                                            Console.Write(" (Available)");
                                        }

                                        Console.ResetColor();
                                        Console.WriteLine();
                                    }
                                }

                                if (availableBooks.Count == 0) // if there are no available books
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("\nNo books are currently available.");
                                    Console.ResetColor();
                                    Console.ReadKey();
                                    break;
                                }
                                Console.WriteLine("\nPress any key to continue...");
                                Console.ReadKey();
                            }
                            else // librarian
                            {
                                Console.Clear();

                                Console.Write("\nEnter book title: ");
                                newTitle = Console.ReadLine();

                                Console.Write("\nEnter author: ");
                                newAuthor = Console.ReadLine();

                                if (newTitle == "" || newAuthor == "") //  If user type empty
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("\nTitle and author cannot be empty.");
                                    Console.ResetColor();
                                }
                                else if (BooksAndAuthors.ContainsKey(newTitle)) // check if the book already exists
                                {
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.WriteLine("\nBook already exists.");
                                    Console.ResetColor();
                                }
                                else // if the book doesn't exist
                                {
                                    BooksAndAuthors[newTitle] = newAuthor;
                                    bookTitles.Add(newTitle);

                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("\nBook added successfully.");
                                    Console.ResetColor();
                                }

                                Console.WriteLine("\nPress any key to continue...");
                                Console.ReadKey();
                            }
                            break;

                        case "2": // case handles borrowing books (students) or viewing all books (librarians)

                            if (role == 1) // student
                            {
                                Console.Clear();
                                Console.WriteLine("\nBorrow Books:");

                                availableBooks.Clear(); // Clear available list for fresh display

                                for (int i = 0; i < bookTitles.Count; i++) // loop through the book titles
                                {
                                    titleHolder = bookTitles[i]; // get the book title

                                    if (!approvedBooks.Contains(titleHolder)) // check if the book is not already borrowed
                                    {

                                        isAlreadyBorrowed = false;
                                        requestIdx = studentBorrowRequests.IndexOf(titleHolder);

                                        if (requestIdx != -1 && requestIdx < requestStudents.Count && requestIdx < requestStatuses.Count && requestStatuses[requestIdx] == "Approved") // check if the book is already borrowed
                                        {
                                            isAlreadyBorrowed = true;
                                        }

                                        if (!isAlreadyBorrowed) // if not borrowed
                                        {
                                            availableBooks.Add(titleHolder); // add to available books list

                                            // display the book title and author
                                            Console.ForegroundColor = ConsoleColor.Yellow;
                                            Console.Write($"\n{availableBooks.Count}. {titleHolder}");
                                            Console.ForegroundColor = ConsoleColor.Green;
                                            Console.Write(" by " + BooksAndAuthors[titleHolder]);
                                            Console.ResetColor();

                                            currentUserPending = false;
                                            currentUserDeclined = false;
                                            anotherUserPending = false;

                                            for (int r = 0; r < studentBorrowRequests.Count; r++) // loop through the student borrow requests
                                            {
                                                if (studentBorrowRequests[r] == titleHolder)
                                                {
                                                    if (requestStudents[r] == userName && requestStatuses[r] == "Pending")
                                                        currentUserPending = true;

                                                    else if (requestStudents[r] == userName && requestStatuses[r] == "Declined")
                                                        currentUserDeclined = true;

                                                    else if (requestStudents[r] != userName && requestStatuses[r] == "Pending")
                                                        anotherUserPending = true;
                                                }
                                            }

                                            if (currentUserPending) // if the current user has a pending request
                                            {
                                                Console.ForegroundColor = ConsoleColor.DarkGray;
                                                Console.Write($" (Request Pending by '{char.ToUpper(userName[0]) + userName.Substring(1).ToLower()}' (You))");
                                                Console.ResetColor();
                                            }
                                            else if (currentUserDeclined) // if the current user has a declined request
                                            {
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.Write(" (Request Declined)");
                                            }
                                            else if (anotherUserPending) // if another user has a pending request
                                            {
                                                Console.ForegroundColor = ConsoleColor.DarkGray;
                                                Console.Write(" (Requested by another user - Pending)");
                                            }
                                            else // if the book is available
                                            {
                                                Console.ForegroundColor = ConsoleColor.Green;
                                                Console.Write(" (Available)");
                                            }

                                            Console.ResetColor();
                                            Console.WriteLine();
                                        }
                                    }
                                }

                                if (availableBooks.Count == 0) // if there are no available books
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("\nNo books are currently available to borrow.");
                                    Console.ResetColor();
                                    Console.ReadKey();
                                    break;
                                }
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.Write("\nEnter the number of the book you want to borrow: ");
                                Console.ResetColor();

                                userInput2 = Console.ReadLine();

                                if (int.TryParse(userInput2, out bookToBorrow) && bookToBorrow >= 1 && bookToBorrow <= availableBooks.Count) // check if the input is valid
                                {
                                    selectedBook = availableBooks[bookToBorrow - 1]; // get the selected book title

                                    Console.Write("\nEnter your name for confirmation: ");
                                    enteredName = Console.ReadLine().Trim().ToUpper();


                                    if (enteredName == userName.ToUpper()) // checker of name and book title match
                                    {
                                        // Check for existing request by this user

                                        existingRequestIndex = -1; // -1 indicates no existing request

                                        for (int i = 0; i < studentBorrowRequests.Count; i++)
                                        {
                                            if (studentBorrowRequests[i] == selectedBook && requestStudents[i] == userName && (requestStatuses[i] == "Pending" || requestStatuses[i] == "Declined")) // check if the book title and user name match
                                            {
                                                existingRequestIndex = i; // get the index of the existing request
                                                break;
                                            }
                                        }

                                        if (existingRequestIndex != -1 && requestStatuses[existingRequestIndex] == "Pending") //  request pending
                                        {
                                            Console.ForegroundColor = ConsoleColor.DarkRed;
                                            Console.WriteLine("\nYou already requested this book (Pending approval).");
                                            Console.ResetColor();
                                        }
                                        else if (existingRequestIndex != -1 && requestStatuses[existingRequestIndex] == "Declined") // request declined
                                        {
                                            requestStatuses[existingRequestIndex] = "Pending";
                                            pendingBookRequests[selectedBook] = userName;

                                            Console.ForegroundColor = ConsoleColor.Green;
                                            Console.WriteLine($"\nBook '{selectedBook}' request resubmitted by '{userName}' (Pending librarian approval).");
                                            Console.ResetColor();

                                        }
                                        else // if no exisitng request
                                        {
                                            // Check if another user already has a pending request
                                            anotherUserHasPending = false;

                                            for (int i = 0; i < studentBorrowRequests.Count; i++) // loop through the student borrow requests
                                            {
                                                if (studentBorrowRequests[i] == selectedBook && requestStudents[i] != userName && requestStatuses[i] == "Pending") // check if another user has a pending request
                                                {
                                                    anotherUserHasPending = true;
                                                    break;
                                                }
                                            }

                                            if (anotherUserHasPending) //  if another user has a pending request
                                            {
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.WriteLine($"\nThis book is already requested by another student (pending librarian approval).");
                                                Console.ResetColor();

                                            }
                                            else //if no other user has a pending request
                                            {
                                                // Add the book to the pending requests

                                                pendingBookRequests[selectedBook] = userName; // add to pending requests
                                                studentBorrowRequests.Add(selectedBook); // add to student borrow requests
                                                requestStudents.Add(userName); // add to request students
                                                requestStatuses.Add("Pending"); // add to request statuses

                                                Console.ForegroundColor = ConsoleColor.Green;
                                                Console.WriteLine($"\nBook '{selectedBook}' requested successfully by '{userName}' (Pending librarian approval).");
                                                Console.ResetColor();
                                            }
                                        }
                                    }
                                    else // if name and book not match
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("\nName does not match to your Logged-In Name. Please try again.");
                                        Console.ResetColor();
                                    }
                                }

                                else // if the input is invalid
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("\nInvalid selection. Please enter a valid book number.");
                                    Console.ResetColor();
                                }

                                Console.WriteLine("\nPress any key to continue...");
                                Console.ReadKey();
                            }
                            else // librarian
                            {
                                Console.Clear();
                                Console.WriteLine("\nAll Books in Library:");

                                for (int i = 0; i < bookTitles.Count; i++) // loop through the book titles
                                {
                                    titleHolder = bookTitles[i]; // get the book title

                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.Write($"\n{i + 1}. {titleHolder}");
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.Write(" by " + BooksAndAuthors[titleHolder]);

                                    if (approvedBooks.Contains(titleHolder))
                                    {
                                        borrower = "Unknown";

                                        for (int j = 0; j < studentBorrowRequests.Count; j++) // find borrower
                                        {
                                            if (studentBorrowRequests[j] == titleHolder && requestStatuses[j] == "Approved")
                                            {
                                                borrower = requestStudents[j];
                                                break;
                                            }
                                        }

                                        Console.ForegroundColor = ConsoleColor.Cyan;
                                        Console.Write($" (Borrowed by {borrower})");
                                    }
                                    else
                                    {
                                        pendingUsers.Clear(); // 🔧 FIX: Clear pending users for this book

                                        for (int r = 0; r < studentBorrowRequests.Count; r++)
                                        {
                                            if (studentBorrowRequests[r] == titleHolder && requestStatuses[r] == "Pending")
                                            {
                                                pendingUser = requestStudents[r];
                                                if (!pendingUsers.Contains(pendingUser))
                                                {
                                                    pendingUsers.Add(pendingUser);
                                                }
                                            }
                                        }

                                        if (pendingUsers.Count > 0)
                                        {
                                            Console.ForegroundColor = ConsoleColor.DarkGray;
                                            Console.Write($" (Request Pending by {string.Join(", ", pendingUsers)})");
                                        }
                                        else
                                        {
                                            Console.ForegroundColor = ConsoleColor.Green;
                                            Console.Write(" (Available)");
                                        }
                                    }

                                    Console.ResetColor();
                                    Console.WriteLine();
                                }

                                Console.WriteLine("\nPress any key to continue...");
                                Console.ReadKey();
                            }
                            break;


                        case "3": // case handles viewing borrowed books (students) or viewing pending requests (librarians)

                            if (role == 1) // Student
                            {
                                Console.Clear();
                                Console.WriteLine("\nYour Book Status:");

                                hasAnyBooks = false;

                                // 1. First show approved (borrowed) books
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine("\nBooks You've Borrowed:");
                                Console.ResetColor();

                                hasApprovedBooks = false;

                                for (int i = 0; i < studentBorrowRequests.Count; i++) // loop through the student borrow requests
                                {
                                    if (requestStudents[i] == userName && requestStatuses[i] == "Approved") // check if the request is approved
                                    {

                                        bookTitle = studentBorrowRequests[i]; // get the book title
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        Console.WriteLine($"\n- {bookTitle} by {BooksAndAuthors[bookTitle]} (Approved)"); // display the book title and author
                                        Console.ResetColor();
                                        hasApprovedBooks = true;
                                        hasAnyBooks = true;
                                    }
                                }

                                if (!hasApprovedBooks) // if there are no approved books
                                {
                                    Console.ForegroundColor = ConsoleColor.DarkGray;
                                    Console.WriteLine("\n You don't have any borrowed books.");
                                    Console.ResetColor();
                                }

                                // 2. Show pending requests
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine("\nYour Pending Requests:");
                                Console.ResetColor();

                                hasPendingBooks = false;

                                for (int i = 0; i < studentBorrowRequests.Count; i++) // loop through the student borrow requests
                                {
                                    if (requestStudents[i] == userName && requestStatuses[i] == "Pending") // check if the request is pending
                                    {
                                        bookTitle = studentBorrowRequests[i]; // get the book title
                                        Console.ForegroundColor = ConsoleColor.White;
                                        Console.WriteLine($"\n- {bookTitle} by {BooksAndAuthors[bookTitle]} (Pending)"); // display the book title and author
                                        Console.ResetColor();
                                        hasPendingBooks = true;
                                        hasAnyBooks = true;
                                    }
                                }

                                if (!hasPendingBooks) // if there are no pending books
                                {
                                    Console.ForegroundColor = ConsoleColor.DarkGray;
                                    Console.WriteLine("\n You don't have any pending requests.");
                                    Console.ResetColor();
                                }

                                // 3. Show declined requests
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine("\nYour Declined Requests:");
                                Console.ResetColor();

                                hasDeclinedBooks = false;

                                for (int i = 0; i < studentBorrowRequests.Count; i++) // loop through the student borrow requests
                                {
                                    if (requestStudents[i] == userName && requestStatuses[i] == "Declined") // check if the request is declined
                                    {
                                        bookTitle = studentBorrowRequests[i]; // get the book title
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine($"\n- {bookTitle} by {BooksAndAuthors[bookTitle]} (Declined)"); // display the book title and author
                                        Console.ResetColor();
                                        hasDeclinedBooks = true;
                                        hasAnyBooks = true;
                                    }
                                }

                                if (!hasDeclinedBooks) // if there are no declined books
                                {
                                    Console.ForegroundColor = ConsoleColor.DarkGray;
                                    Console.WriteLine("\n You don't have any declined requests.");
                                    Console.ResetColor();
                                }

                                if (!hasAnyBooks) // if there are no books borrowed or requested
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("\nYou have no book activity to display.");
                                    Console.ResetColor();
                                }

                                Console.WriteLine("\nPress any key to continue...");
                                Console.ReadKey();
                            }
                            else // Librarian
                            {
                                Console.Clear();
                                Console.WriteLine("\nPending Borrow Requests:");

                                hasPending = false; // reset flag

                                for (int i = 0; i < studentBorrowRequests.Count; i++) // loop through the pending requests
                                {
                                    if (requestStatuses[i] == "Pending")  // check if the request is pending
                                    {
                                        Console.ForegroundColor = ConsoleColor.Cyan;
                                        Console.WriteLine($"\n- {studentBorrowRequests[i]} requested by {requestStudents[i]} (Pending)"); // display the book title and requester
                                        Console.ResetColor();
                                        hasPending = true;
                                    }
                                }

                                if (!hasPending) // if not pending
                                {
                                    Console.ForegroundColor = ConsoleColor.DarkGray;
                                    Console.WriteLine("\nNo pending requests.");
                                    Console.ResetColor();
                                }

                                Console.WriteLine("\nPress any key to continue...");
                                Console.ReadKey();
                            }
                            break;

                        case "4": // return a book (students) or approve/decline requests (librarians)

                            if (role == 1) // student
                            {
                                Console.Clear();
                                Console.WriteLine("\nBorrowed Books:");


                                if (!studentBorrowedBooks.ContainsKey(userName) || studentBorrowedBooks[userName].Count == 0) // check if the user has borrowed books
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("\nNo books borrowed.\n");
                                    Console.ResetColor();
                                }
                                else // if the user has borrowed books
                                {
                                    for (int i = 0; i < studentBorrowedBooks[userName].Count; i++) // loop through the borrowed books
                                    {
                                        title = studentBorrowedBooks[userName][i]; // get the book title
                                        Console.ForegroundColor = ConsoleColor.Yellow;
                                        Console.Write($"\n{i + 1}. {title}"); // display the book title
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        Console.Write(" by " + BooksAndAuthors[title]); // display the author
                                        Console.ResetColor();
                                        Console.WriteLine();
                                    }

                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.Write("\nEnter the number of the book you want to return: ");
                                    Console.ResetColor();
                                    userInput3 = Console.ReadLine();

                                    if (int.TryParse(userInput3, out bookIndex) && bookIndex >= 1 && bookIndex <= studentBorrowedBooks[userName].Count) // check if the input is valid
                                    {
                                        ReturnNow = studentBorrowedBooks[userName][bookIndex - 1]; // get the selected book title

                                        if (approvedBooks.Contains(ReturnNow) && borrowedBooksTracker[ReturnNow] == userName) // check if the book is approved and borrowed by the user
                                        {

                                            studentBorrowedBooks[userName].Remove(ReturnNow); // Remove from the borrowed books list

                                            approvedBooks.Remove(ReturnNow); // Remove from approved books

                                            borrowedBooksTracker.Remove(ReturnNow); // Remove from tracker

                                            requestIndex = studentBorrowRequests.IndexOf(ReturnNow); // get the index of the request

                                            if (requestIndex != -1 && requestIndex < requestStudents.Count && requestIndex < requestStatuses.Count) // check if the request index is valid
                                            {
                                                studentBorrowRequests.RemoveAt(requestIndex); // remove from requests

                                                requestStudents.RemoveAt(requestIndex); // remove from request students

                                                requestStatuses.RemoveAt(requestIndex); // remove from request statuses
                                            }

                                            Console.ForegroundColor = ConsoleColor.Green;
                                            Console.WriteLine("\nBook returned successfully.");
                                            Console.ResetColor();
                                        }
                                        else // display error if the book is not approved or borrowed by the user
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine("\nYou can't return this book — it's still pending approval or you are not the one who borrowed this book.");
                                            Console.ResetColor();
                                        }
                                    }
                                    else // if the input is invalid
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("\nInvalid selection.");
                                        Console.ResetColor();
                                    }



                                    Console.ResetColor();
                                    userInput3 = Console.ReadLine();


                                    if (int.TryParse(userInput3, out bookIndex) && bookIndex >= 1 && bookIndex <= borrowedBooks.Count) // check if the input is valid
                                    {
                                        ReturnNow = borrowedBooks[bookIndex - 1]; // get the selected book title



                                        if (studentBorrowedBooks.ContainsKey(userName) && studentBorrowedBooks[userName].Contains(ReturnNow) && approvedBooks.Contains(ReturnNow) && borrowedBooksTracker.ContainsKey(ReturnNow) && borrowedBooksTracker[ReturnNow] == userName) // check if the book is borrowed by the user
                                        {
                                            // Remove from the borrowed books list

                                            studentBorrowedBooks[userName].Remove(ReturnNow); // Remove from the borrowed books list

                                            approvedBooks.Remove(ReturnNow); // Remove from approved books

                                            pendingBookRequests.Remove(ReturnNow); // Remove from pending requests

                                            borrowedBooksTracker.Remove(ReturnNow); // Remove from tracker

                                            requestIndex = studentBorrowRequests.IndexOf(ReturnNow); // get the index of the request

                                            if (requestIndex != -1 && requestIndex < requestStudents.Count && requestIndex < requestStatuses.Count) // check if the request index is valid
                                            {
                                                studentBorrowRequests.RemoveAt(requestIndex); // remove from requests

                                                requestStudents.RemoveAt(requestIndex); // remove from request students

                                                requestStatuses.RemoveAt(requestIndex); // remove from request statuses
                                            }

                                            Console.ForegroundColor = ConsoleColor.Green;
                                            Console.WriteLine("\nBook returned successfully.");
                                            Console.ResetColor();
                                        }
                                       
                                        else // it's still pending approval
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine("\nYou can't return this book — it's still pending approval or you are not the one who borrowed this book.");
                                            Console.ResetColor();
                                        }

                                    }


                                }
                                Console.WriteLine("Press any key to continue...");
                                Console.ReadKey();
                                break;
                            }

                            else // librarian
                            {
                                Console.Clear();
                                Console.WriteLine("\nPending Requests:");

                                if (studentBorrowRequests.Count == 0) // check if there are no requests
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("\nNo pending requests.");
                                    Console.ResetColor();
                                    Console.ReadKey();
                                }
                                else // there is a request
                                {
                                    pendingIndices.Clear(); // clear the pending indices list


                                    for (int i = 0; i < studentBorrowRequests.Count; i++) //loop through the borrow requests
                                    {
                                        if (requestStatuses[i] == "Pending")
                                        {
                                            pendingIndices.Add(i); // store actual index
                                            Console.ForegroundColor = ConsoleColor.Cyan;
                                            Console.WriteLine($"\n{pendingIndices.Count}. {studentBorrowRequests[i]} requested by {requestStudents[i]} (Status: {requestStatuses[i]})"); // display the book title and requester
                                            Console.ResetColor();
                                        }
                                    }

                                    if (pendingIndices.Count == 0)
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("\nNo pending requests.");
                                        Console.ResetColor();
                                        Console.ReadKey();
                                        return;
                                    }

                                    Console.Write("\nEnter the number of the request to approve or decline (press any key to exit): ");
                                    ADinput = Console.ReadLine();

                                    if (int.TryParse(ADinput, out selection) && selection > 0 && selection <= pendingIndices.Count) // check if the input is valid
                                    {
                                        c = pendingIndices[selection - 1]; // get the actual index of the selected request

                                        selectedBook = studentBorrowRequests[c]; // get the book title

                                        requester = requestStudents[c]; // get the requester name

                                        currentStatus = requestStatuses[c]; // get the current status

                                        Console.Clear();
                                        Console.ForegroundColor = ConsoleColor.Cyan;
                                        Console.WriteLine($"\nYou selected: {selectedBook} requested by {requester} (Status: {currentStatus})");
                                        Console.ResetColor();

                                        Console.Write("\nApprove or Decline this request? (A/D): ");
                                        approvalInput = Console.ReadLine().Trim().ToUpper();

                                        if (approvalInput == "A") // if approved
                                        {
                                            requestStatuses[c] = "Approved";

                                            if (!studentBorrowedBooks.ContainsKey(requester)) // check if the student has borrowed books
                                            {
                                                studentBorrowedBooks[requester] = new List<string>(); // create a new list for the student
                                            }

                                            studentBorrowedBooks[requester].Add(selectedBook); // add the book to the borrower's list

                                            borrowedBooksTracker[selectedBook] = requester; // add the book to the tracker

                                            approvedBooks.Add(selectedBook); // add the book to approved books

                                            Console.ForegroundColor = ConsoleColor.Green;
                                            Console.WriteLine($"\nRequest approved for '{requester}' to borrow the book '{selectedBook}'");
                                            Console.ResetColor();
                                        }
                                        else if (approvalInput == "D") // if declined
                                        {
                                            requestStatuses[c] = "Declined";

                                            if (pendingBooks.Contains(selectedBook)) // check if the book is in pending books
                                            {

                                                pendingBooks.Remove(selectedBook);
                                            }

                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine($"\nRequest for book '{selectedBook}' declined for '{requester}'.");
                                            Console.ResetColor();
                                        }
                                        else
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine("\nInvalid input. Please enter 'A' or 'D'.");
                                            Console.ResetColor();
                                        }
                                    }
                                }
                            }

                            Console.WriteLine("\nPress any key to continue...");
                            Console.ReadKey();
                            break;


                        case "5": // case handles logging out for both students and librarians

                            keepRunning = false;
                            Console.WriteLine("\nLogging out...");
                            Thread.Sleep(1000);
                            break;

                        default: // if the input is invalid
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nInvalid option.");
                            Console.ResetColor();
                            Console.ReadKey();
                            break;

                    }
                }

            }
            while (!exitProgram); // end

        }
    }
}
