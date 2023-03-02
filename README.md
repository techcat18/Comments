# Comments application

## Summary

This applicaton allows users to view and create comments. In order to create a comment users need to sign up or log in if they already have an account. Authorized users can also reply to other comments and edit theirs. It is possible to add an attachment to the comment. A valid attachment is a file of .png, .jpg, .gif or .txt (less than 100kb) extension. There is an opportunity to include html tags in comments (i, strong, code, a).


## How to run

- Clone the repository using the following command in the terminal: ``git clone https://github.com/bulkedUpCat/Comments.git``
- Make sure you have Docker installed on your machine before proceeding.
- Go to the same folder as docker-compose.yml file and run the following command in the terminal:
  ``docker compose up --build`` (use admin rights).
  This process may take some time if you run it for the first time, since it will pull all required images.
- Go to http://localhost:100.
- Sign up using a link in the top right corner (or go to http://localhost:100/auth/signup).

## Technologies used

- ASP .NET API
- Angular
- SQL Server
- Docker
- Microsoft Azure
