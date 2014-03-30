create table `log-events``fatec-log` (
	id int primary key auto_increment,
	eventType varchar(100) not null,
	`date` datetime not null,
	`event` longtext not null 
)
