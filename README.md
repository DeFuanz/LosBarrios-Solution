# LosBarriosEvents

For testing, please use the following accounts already created, or feel free to create your own.

Admin Account: 
admin@email.com
Password1!

Student Account:
student@email.com
Password1!

Speaker Account:
speaker@email.com
Password1!

Introduction:

Brief introduction of LosBarriosEvents.
LosBarrios is a nonprofit organization that helps guide highschool students towards different career paths.
Student have the option to choose from 2 year colleges, 4 year colleges and tradeschools.
They are guided through speakers on choices for career paths with guidance from volunteers and admin.

Description of team members,
    Hogan Karr, John Hernandez, Andrew Alatorre, John Defore
    
# Solid Principles & architecture
***
Throughout the project we were able to implement solid principles here and there but not fully. While we would have liked to implement a better architecture, we did not prioritize as we didn't discuss that knowledge as a team until further in the project. We did however, implement a few principles such as breaking down classes to single functionality, inhertiting classes and overriding such as page models and custom identity users, as well as tried to not repeat ourselves (at least on the same backend page by making custom methods). We would have been better served by creating a class librarry and seperating data and logic similar to an api but chose to prioritize the functionaility first. 

# User Stories
***
Front of Card:
Feature: LosBarrios Bios

As a Student
I want to look at Speakers Bio
So that I can get understand their background of who they are.

Back of Card:

Given that students must choose from speaker bios tab
When they must choose from speaker bios
Then return to homepage to view other options for events.

***
Feature: LosBarrios Events

As an Admin
I want to create events with sessions
So that students, speakers and volunteers can be group into the events.

Back of Card:

Given that admin must link events to sessions
When they tie them together to display data
Then reports are generated so that students, speakers and volunteers can view.

***
Feature: Speaker forms

As a speaker
I want go to the speaker form page
So that I can fill out a form of requirements I need in the room.

Back of Card:

Given that speaker must fill out a form
When registering for events to be a part of
Then admin can view the report so see which rooms to put users in.

***
Feature: Feedback Page

As an admin
I want go provide feedback option about sessions
So that we can view performance of the events.

Back of Card:

Given that students must fill out a feedback form
When events end
Then admin can view the report so see which rooms to put users in.


Description of team members,
    Hogan Karr, John Hernandez, Andrew Alatorre, John Defore
