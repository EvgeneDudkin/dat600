package main

import (
	"fmt"
	"math/rand"
	"time"
)

// Main function
func main() {
	arraySizes := []int{50000, 100000, 500000, 10000000, 30000000}
	for i := 0; i < len(arraySizes); i++ {
		size := arraySizes[i]

		array := generateRandomArray(size, -1000, 1000)
		// watch := stopwatch.Start()
		start := time.Now()

		quickSort(array, 0, size-1)
		// watch.Stop()
		elapsed := time.Since(start)
		fmt.Printf("Size: %v, Elapsed: %s\n", size, elapsed)
		//fmt.Printf("Size: %v, Milliseconds elapsed: %s\n", size, watch.Milliseconds())
	}
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
