<?php
	include_once '../../config/Database.php';
	include_once '../../models/Player.php';

	$database = new Database();
	$db = $database->connect();

	$player = new player($db);

	$result = $player->read();

	$num = $result->rowCount();

	if($num > 0){
		$players_arr = array();
		//$players_arr['data'] = array();

		while($row = $result->fetch(PDO::FETCH_ASSOC)){
			extract($row);

			$player_item = array(
				'id' => $id,
				'name' => $name,
				'score' => $score,
			);

			array_push($players_arr, $player_item);
		}
	}
?>

<html>
 <head>
 	<h1 style="text-align: center;">SCOREBOARD</h1>
 </head>
 <body>

 	<table style="width:100%">
 	 <tr>
	    <th>RANK</th>
	    <th>NOME</th> 
	    <th>SCORE</th>
	  </tr>

	<?php 
		$i = 1;
		foreach($players_arr as $p){?>
		<tr>
			<th><?=$i?></th>
			<th><?=$p['name']?></th>
			<th><?=$p['score']?></th>
		</tr>
	<?php
	$i++;
	}?>

	</table>

 </body>
</html>


