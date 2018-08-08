<?php
	class Player{

		private $conn;
		private $table = 'players';

		public $id;
		public $name;
		public $score;

		public function __construct($db){
			$this->conn = $db;
		}

		public function read(){
			$query = 'SELECT 
			id,
			name,
			score
			FROM ' . $this->table . ' ORDER BY score DESC';

			$stmt = $this->conn->prepare($query);

			$stmt->execute();

			return $stmt;
		}

		public function read_single(){
			$query = 'SELECT 
			id,
			name,
			score,
			FROM ' . $this->table . ' WHERE id = ? LIMIT 0,1';
			$stmt = $this->conn->prepare($query);
			$stmt->bindParam(1, $this->id);
			$stmt->execute();

			$row = $stmt->fetch(PDO::FETCH_ASSOC);

			$this->name = $row['name'];
			$this->score = $row['score'];

		}

		public function create(){
			$query = 'INSERT INTO ' .$this->table.
			' SET name = :name,
				score = :score';

			$stmt = $this->conn->prepare($query);

			$this->name = htmlspecialchars(strip_tags($this->name));
			$this->score = htmlspecialchars(strip_tags($this->score));

			$stmt->bindParam(':name', $this->name);
			$stmt->bindParam(':score', $this->score);

			if($stmt->execute()){
				return true;
			}

			printf("Error: %s.\n", $stmt->error);
			return false;
		}
	}