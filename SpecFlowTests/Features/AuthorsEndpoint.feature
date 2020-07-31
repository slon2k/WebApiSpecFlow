Feature: Authors
	Testing CRUD operations on "/api/authors/" endpoint

@get @authors
Scenario: I can get a list of all authors 
	Given I am a client
	When I make request to "/api/authors"
	Then the response status code should be "Ok"

@get @author
Scenario: I can get a single author
	Given I am a client
	When I make request to "/api/authors/1"
	Then the response status code should be "Ok"

@post @author
Scenario: I can add a new author
	Given I am a client
	When I make create request to "/api/authors" with data
	| FirstName | LastName | Genre           |
	| Douglas   | Adams    | Science fiction |
	Then the response status code should be "Created"

@put @author
Scenario: I can update the author
	Given I am a client
	When I make chenge request to "/api/authors/3" with data
	| FirstName | LastName | Genre           |
	| Douglas   | Adams    | Modified  |
	Then the response status code should be "NoContent"