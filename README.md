# Flashcards Application

## Project Description
The **Flashcards** application allows users to create and manage flashcards organized in stacks, study them, and track their progress through study sessions. Each flashcard must belong to a stack, and all study sessions are logged for future reference. The system is designed with clean data management practices, ensuring flashcards and study sessions are deleted when their associated stacks are removed.

## Features
- **Stacks and Flashcards**: Users can create flashcard stacks, with each stack containing multiple flashcards. Stacks have unique names, and deleting a stack also removes its associated flashcards.
- **Study Sessions**: Users can engage in study sessions with the stacks they create. Study sessions track the date and the user's score. All study sessions are saved and cannot be modified or deleted after they are created.
- **Flashcard ID Handling**: When displaying flashcards to the user, IDs are renumbered sequentially starting from 1, with no gaps between them, even after deletions.
- **Data Management**: The project uses foreign keys to link flashcards to their respective stacks, and study sessions to stacks. Deleting a stack automatically removes its flashcards and study sessions.

## Technologies
- **Backend**: .NET (C#)
- **Database**: SQL Server 
- **Data Transfer Objects (DTOs)**: Used to manage how data is presented to users (e.g., flashcards are shown without their stack IDs).
- **Entity Framework Core** (optional) or **raw SQL** for data persistence.

## Installation

### 1. Clone the repository
```bash
git clone https://github.com/mateuszsiwy/Flashcards
cd Flashcards
