create table log (
	id int primary key auto_increment,
	applicationName longtext not null,
	message longtext not null,
	url longtext null,
	ipAddress longtext null,
	username varchar(255) null default null,
	createdOn datetime not null,
	details longtext null
)
