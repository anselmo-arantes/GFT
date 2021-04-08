Hello!

I just would like to explain some stuff for a better evaluation of the questions.

Question 1:
	- To run the program, you must run "script.sql" on your database before. This will create and insert data in the tables.
	- You will need to set your database connection in the appsetting.json file in order to get it running.
	- I used "Dapper" as ORM, so you may need to get it.
	- In the "Main" of the program, you can change the values for testing different scenarios.
	- The console prints the output results based on the input sequence. 
	
Question 2:
	- Please, run "DDL_trades.sql" first. This will create and insert data in the tables.
	- After that, you can create and execute the procedure through "tradesCategoriesProcedure.sql".
	- The idea here is that we use the same table for input and results, you got the input (CLIENT_SECTOR, CLIENT_VALUE) first and then, the procedure will read this information and save the result in the RISK_RESULT field as well as its analysis date. 
	
Obs.: The number "922337203685477.58" is the maximum of Money datatype. So, take it as infinity.
	
Ps.: Both question solutions end up quite simple. This week's been quite busy for me, so I couldn't get much time to develop a better and more modern solution.

Thanks.
