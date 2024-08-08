-- phpMyAdmin SQL Dump
-- version 5.1.2
-- https://www.phpmyadmin.net/
--
-- Host: localhost:3306
-- Generation Time: Aug 02, 2024 at 09:43 PM
-- Server version: 5.7.24
-- PHP Version: 8.3.9

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `unimealplanner_db`
--

-- --------------------------------------------------------

--
-- Table structure for table `categories`
--

CREATE TABLE `categories` (
  `category_id` int(11) NOT NULL,
  `category_name` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `categories`
--

INSERT INTO `categories` (`category_id`, `category_name`) VALUES
(1, 'Breakfast'),
(2, 'Lunch'),
(3, 'Dinner'),
(4, 'Snacks'),
(5, 'Beverages'),
(6, 'Desserts'),
(7, 'Salads'),
(8, 'Appetizers'),
(9, 'Main Course'),
(10, 'Side Dish');

-- --------------------------------------------------------

--
-- Table structure for table `ingredients`
--

CREATE TABLE `ingredients` (
  `ingredient_id` int(11) NOT NULL,
  `ingredient_name` varchar(100) NOT NULL,
  `is_allergen` bit(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `ingredients`
--

INSERT INTO `ingredients` (`ingredient_id`, `ingredient_name`, `is_allergen`) VALUES
(1, 'Flour', b'1'),
(2, 'Lettuce', b'0'),
(3, 'Beef', b'0'),
(4, 'Milk', b'1'),
(5, 'Eggs', b'1'),
(6, 'Tomato', b'0'),
(7, 'Cheese', b'1'),
(8, 'Chicken', b'0'),
(9, 'Pasta', b'1'),
(10, 'Potatoes', b'0');

-- --------------------------------------------------------

--
-- Table structure for table `mealplans`
--

CREATE TABLE `mealplans` (
  `plan_id` int(11) NOT NULL,
  `plan_name` enum('Gold','Silver','Bronze') NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `mealplans`
--

INSERT INTO `mealplans` (`plan_id`, `plan_name`) VALUES
(1, 'Gold'),
(2, 'Silver'),
(3, 'Bronze');

-- --------------------------------------------------------

--
-- Table structure for table `meals`
--

CREATE TABLE `meals` (
  `meal_id` int(11) NOT NULL,
  `meal_plan_id` int(11) DEFAULT NULL,
  `recipe_id` int(11) DEFAULT NULL,
  `meal_date` date DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `meals`
--

INSERT INTO `meals` (`meal_id`, `meal_plan_id`, `recipe_id`, `meal_date`) VALUES
(1, 1, 1, '2024-07-15'),
(2, 2, 2, '2024-07-16'),
(3, 3, 3, '2024-07-17'),
(4, 1, 4, '2024-07-18'),
(5, 2, 5, '2024-07-19'),
(6, 3, 6, '2024-07-20'),
(7, 1, 7, '2024-07-21'),
(8, 2, 8, '2024-07-22'),
(9, 3, 9, '2024-07-23'),
(10, 1, 10, '2024-07-24');

-- --------------------------------------------------------

--
-- Table structure for table `recipeingredients`
--

CREATE TABLE `recipeingredients` (
  `recipe_id` int(11) NOT NULL,
  `ingredient_id` int(11) NOT NULL,
  `quantity` varchar(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `recipeingredients`
--

INSERT INTO `recipeingredients` (`recipe_id`, `ingredient_id`, `quantity`) VALUES
(1, 1, '2 cups'),
(2, 2, '1 head'),
(3, 3, '1 lb'),
(4, 4, '2 cups'),
(5, 5, '3'),
(6, 6, '2'),
(7, 7, '1 cup'),
(8, 8, '2 slices'),
(9, 9, '200 grams'),
(10, 10, '3 pieces');

-- --------------------------------------------------------

--
-- Table structure for table `recipes`
--

CREATE TABLE `recipes` (
  `recipe_id` int(11) NOT NULL,
  `recipe_name` varchar(100) NOT NULL,
  `category_id` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `recipes`
--

INSERT INTO `recipes` (`recipe_id`, `recipe_name`, `category_id`) VALUES
(1, 'Pancakes', 1),
(2, 'Salad', 2),
(3, 'Steak', 3),
(4, 'Fruit Smoothie', 5),
(5, 'Chocolate Cake', 6),
(6, 'Caesar Salad', 7),
(7, 'Spring Rolls', 8),
(8, 'Grilled Cheese Sandwich', 2),
(9, 'Spaghetti', 9),
(10, 'French Fries', 10);

-- --------------------------------------------------------

--
-- Table structure for table `recipetags`
--

CREATE TABLE `recipetags` (
  `recipe_id` int(11) NOT NULL,
  `tag_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `recipetags`
--

INSERT INTO `recipetags` (`recipe_id`, `tag_id`) VALUES
(1, 1),
(2, 2),
(3, 3),
(4, 4),
(5, 5),
(6, 6),
(7, 7),
(8, 8),
(9, 9),
(10, 10);

-- --------------------------------------------------------

--
-- Table structure for table `remindernotes`
--

CREATE TABLE `remindernotes` (
  `note_id` int(11) NOT NULL,
  `student_id` int(11) DEFAULT NULL,
  `teacher_id` int(11) DEFAULT NULL,
  `note_text` text
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `remindernotes`
--

INSERT INTO `remindernotes` (`note_id`, `student_id`, `teacher_id`, `note_text`) VALUES
(1, 1, 1, 'Reminder to check on meal plans.'),
(2, 2, 2, 'Reminder for allergy details.'),
(3, 3, 3, 'Check for special dietary requirements.'),
(4, 4, 4, 'Discuss project progress.'),
(5, 5, 5, 'Follow up on attendance.'),
(6, 6, 6, 'Prepare for next exam.'),
(7, 7, 7, 'Complete assignment by next week.'),
(8, 8, 8, 'Arrange a meeting with the counselor.'),
(9, 9, 9, 'Discuss performance improvement.'),
(10, 10, 10, 'Prepare for the upcoming test.');

-- --------------------------------------------------------

--
-- Table structure for table `studentmealplans`
--

CREATE TABLE `studentmealplans` (
  `student_id` int(11) NOT NULL,
  `plan_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `studentmealplans`
--

INSERT INTO `studentmealplans` (`student_id`, `plan_id`) VALUES
(1, 1),
(4, 1),
(7, 1),
(10, 1),
(2, 2),
(5, 2),
(8, 2),
(3, 3),
(6, 3),
(9, 3);

-- --------------------------------------------------------

--
-- Table structure for table `students`
--

CREATE TABLE `students` (
  `student_id` int(11) NOT NULL,
  `first_name` varchar(50) NOT NULL,
  `last_name` varchar(50) NOT NULL,
  `email` varchar(100) NOT NULL,
  `phone_number` varchar(20) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `students`
--

INSERT INTO `students` (`student_id`, `first_name`, `last_name`, `email`, `phone_number`) VALUES
(1, 'Alice', 'Johnson', 'alicejohnson@example.com', '345-678-9012'),
(2, 'Bob', 'Williams', 'bobwilliams@example.com', '456-789-0123'),
(3, 'Charlie', 'Brown', 'charliebrown@example.com', '567-890-1234'),
(4, 'David', 'Wilson', 'davidwilson@example.com', '678-901-2345'),
(5, 'Eva', 'Martinez', 'evamartinez@example.com', '789-012-3456'),
(6, 'Frank', 'Miller', 'frankmiller@example.com', '890-123-4567'),
(7, 'Grace', 'Clark', 'graceclark@example.com', '901-234-5678'),
(8, 'Henry', 'Walker', 'henrywalker@example.com', '012-345-6789'),
(9, 'Ivy', 'Young', 'ivyyoung@example.com', '123-456-7891'),
(10, 'Jack', 'Hall', 'jackhall@example.com', '234-567-8902');

-- --------------------------------------------------------

--
-- Table structure for table `tags`
--

CREATE TABLE `tags` (
  `tag_id` int(11) NOT NULL,
  `tag_name` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `tags`
--

INSERT INTO `tags` (`tag_id`, `tag_name`) VALUES
(1, 'Vegetarian'),
(2, 'Gluten-Free'),
(3, 'Dairy-Free'),
(4, 'Vegan'),
(5, 'Nut-Free'),
(6, 'Low-Carb'),
(7, 'High-Protein'),
(8, 'Keto'),
(9, 'Sugar-Free'),
(10, 'Organic');

-- --------------------------------------------------------

--
-- Table structure for table `teachermealplans`
--

CREATE TABLE `teachermealplans` (
  `teacher_id` int(11) NOT NULL,
  `plan_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `teachermealplans`
--

INSERT INTO `teachermealplans` (`teacher_id`, `plan_id`) VALUES
(1, 1),
(4, 1),
(7, 1),
(10, 1),
(2, 2),
(5, 2),
(8, 2),
(3, 3),
(6, 3),
(9, 3);

-- --------------------------------------------------------

--
-- Table structure for table `teachers`
--

CREATE TABLE `teachers` (
  `teacher_id` int(11) NOT NULL,
  `first_name` varchar(50) NOT NULL,
  `last_name` varchar(50) NOT NULL,
  `email` varchar(100) NOT NULL,
  `phone_number` varchar(20) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `teachers`
--

INSERT INTO `teachers` (`teacher_id`, `first_name`, `last_name`, `email`, `phone_number`) VALUES
(1, 'John', 'Doe', 'johndoe@example.com', '123-456-7890'),
(2, 'Jane', 'Smith', 'janesmith@example.com', '234-567-8901'),
(3, 'Mark', 'Taylor', 'marktaylor@example.com', '345-678-9012'),
(4, 'Lucy', 'Liu', 'lucyliu@example.com', '456-789-0123'),
(5, 'Michael', 'Jordan', 'michaeljordan@example.com', '567-890-1234'),
(6, 'Emily', 'Davis', 'emilydavis@example.com', '678-901-2345'),
(7, 'Daniel', 'Roberts', 'danielroberts@example.com', '789-012-3456'),
(8, 'Jessica', 'Lee', 'jessicalee@example.com', '890-123-4567'),
(9, 'Oliver', 'Brown', 'oliverbrown@example.com', '901-234-5678'),
(10, 'Sophia', 'Garcia', 'sophiagarcia@example.com', '012-345-6789');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `categories`
--
ALTER TABLE `categories`
  ADD PRIMARY KEY (`category_id`);

--
-- Indexes for table `ingredients`
--
ALTER TABLE `ingredients`
  ADD PRIMARY KEY (`ingredient_id`);

--
-- Indexes for table `mealplans`
--
ALTER TABLE `mealplans`
  ADD PRIMARY KEY (`plan_id`);

--
-- Indexes for table `meals`
--
ALTER TABLE `meals`
  ADD PRIMARY KEY (`meal_id`),
  ADD KEY `meal_plan_id` (`meal_plan_id`),
  ADD KEY `recipe_id` (`recipe_id`);

--
-- Indexes for table `recipeingredients`
--
ALTER TABLE `recipeingredients`
  ADD PRIMARY KEY (`recipe_id`,`ingredient_id`),
  ADD KEY `ingredient_id` (`ingredient_id`);

--
-- Indexes for table `recipes`
--
ALTER TABLE `recipes`
  ADD PRIMARY KEY (`recipe_id`),
  ADD KEY `category_id` (`category_id`);

--
-- Indexes for table `recipetags`
--
ALTER TABLE `recipetags`
  ADD PRIMARY KEY (`recipe_id`,`tag_id`),
  ADD KEY `tag_id` (`tag_id`);

--
-- Indexes for table `remindernotes`
--
ALTER TABLE `remindernotes`
  ADD PRIMARY KEY (`note_id`),
  ADD KEY `student_id` (`student_id`),
  ADD KEY `teacher_id` (`teacher_id`);

--
-- Indexes for table `studentmealplans`
--
ALTER TABLE `studentmealplans`
  ADD PRIMARY KEY (`student_id`,`plan_id`),
  ADD KEY `plan_id` (`plan_id`);

--
-- Indexes for table `students`
--
ALTER TABLE `students`
  ADD PRIMARY KEY (`student_id`);

--
-- Indexes for table `tags`
--
ALTER TABLE `tags`
  ADD PRIMARY KEY (`tag_id`);

--
-- Indexes for table `teachermealplans`
--
ALTER TABLE `teachermealplans`
  ADD PRIMARY KEY (`teacher_id`,`plan_id`),
  ADD KEY `plan_id` (`plan_id`);

--
-- Indexes for table `teachers`
--
ALTER TABLE `teachers`
  ADD PRIMARY KEY (`teacher_id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `categories`
--
ALTER TABLE `categories`
  MODIFY `category_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT for table `ingredients`
--
ALTER TABLE `ingredients`
  MODIFY `ingredient_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT for table `mealplans`
--
ALTER TABLE `mealplans`
  MODIFY `plan_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `meals`
--
ALTER TABLE `meals`
  MODIFY `meal_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT for table `recipes`
--
ALTER TABLE `recipes`
  MODIFY `recipe_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT for table `remindernotes`
--
ALTER TABLE `remindernotes`
  MODIFY `note_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT for table `students`
--
ALTER TABLE `students`
  MODIFY `student_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT for table `tags`
--
ALTER TABLE `tags`
  MODIFY `tag_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT for table `teachers`
--
ALTER TABLE `teachers`
  MODIFY `teacher_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `meals`
--
ALTER TABLE `meals`
  ADD CONSTRAINT `meals_ibfk_1` FOREIGN KEY (`meal_plan_id`) REFERENCES `mealplans` (`plan_id`),
  ADD CONSTRAINT `meals_ibfk_2` FOREIGN KEY (`recipe_id`) REFERENCES `recipes` (`recipe_id`);

--
-- Constraints for table `recipeingredients`
--
ALTER TABLE `recipeingredients`
  ADD CONSTRAINT `recipeingredients_ibfk_1` FOREIGN KEY (`recipe_id`) REFERENCES `recipes` (`recipe_id`),
  ADD CONSTRAINT `recipeingredients_ibfk_2` FOREIGN KEY (`ingredient_id`) REFERENCES `ingredients` (`ingredient_id`);

--
-- Constraints for table `recipes`
--
ALTER TABLE `recipes`
  ADD CONSTRAINT `recipes_ibfk_1` FOREIGN KEY (`category_id`) REFERENCES `categories` (`category_id`);

--
-- Constraints for table `recipetags`
--
ALTER TABLE `recipetags`
  ADD CONSTRAINT `recipetags_ibfk_1` FOREIGN KEY (`recipe_id`) REFERENCES `recipes` (`recipe_id`),
  ADD CONSTRAINT `recipetags_ibfk_2` FOREIGN KEY (`tag_id`) REFERENCES `tags` (`tag_id`);

--
-- Constraints for table `remindernotes`
--
ALTER TABLE `remindernotes`
  ADD CONSTRAINT `remindernotes_ibfk_1` FOREIGN KEY (`student_id`) REFERENCES `students` (`student_id`),
  ADD CONSTRAINT `remindernotes_ibfk_2` FOREIGN KEY (`teacher_id`) REFERENCES `teachers` (`teacher_id`);

--
-- Constraints for table `studentmealplans`
--
ALTER TABLE `studentmealplans`
  ADD CONSTRAINT `studentmealplans_ibfk_1` FOREIGN KEY (`student_id`) REFERENCES `students` (`student_id`),
  ADD CONSTRAINT `studentmealplans_ibfk_2` FOREIGN KEY (`plan_id`) REFERENCES `mealplans` (`plan_id`);

--
-- Constraints for table `teachermealplans`
--
ALTER TABLE `teachermealplans`
  ADD CONSTRAINT `teachermealplans_ibfk_1` FOREIGN KEY (`teacher_id`) REFERENCES `teachers` (`teacher_id`),
  ADD CONSTRAINT `teachermealplans_ibfk_2` FOREIGN KEY (`plan_id`) REFERENCES `mealplans` (`plan_id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
