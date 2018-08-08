<?php

	header('Access-Control-Allow-Origin: *');
	header('Content-Type: application/json');
	header('Access-Control-Allow-Methods: POST');
	header('Access-Control-Allow-Headers: Access-Control-Allow-Headers, Content-Type, Access-Control-Allow-Methods, Authorization, X-Requested-With');

	include_once '../../config/Database.php';
	include_once '../../models/Player.php';

	$database = new Database();
	$db = $database->connect();

	$player = new Player($db);

	$data = json_decode(file_get_contents("php://input"));

	$player->name = $data->name;
	$player->score = $data->score;

	if($player->create()){
		echo json_encode(array('message' => 'Player Created'));
	}else{
		echo json_encode(array('message' => 'Player Not Created'));
	}