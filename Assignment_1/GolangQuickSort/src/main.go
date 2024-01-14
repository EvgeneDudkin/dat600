// First Go program
package main

import (
	"fmt"
	"math/rand"
	"time"
)

// Main function
func main() {
	array := generateRandomArray(10, -100, 100)
	fmt.Println(array)
}

func generateRandomArray(length int, min int, max int) []int {
	// Seed the random number generator to ensure different results on each run
	rand.Seed(time.Now().UnixNano())

	// Create an array with the specified length
	result := make([]int, length)

	// Populate the array with random integers
	for i := 0; i < length; i++ {
		result[i] = rand.Intn(max-min) + min // Adjust the range as needed
	}

	return result
}
