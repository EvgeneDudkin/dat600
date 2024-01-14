package main

import (
	"reflect"
	"testing"
)

func TestQuickSort(t *testing.T) {
	// Helper function to check if the array is sorted
	isSorted := func(array []int) bool {
		for i := 1; i < len(array); i++ {
			if array[i-1] > array[i] {
				return false
			}
		}
		return true
	}

	tests := []struct {
		name   string
		input  []int
		output []int
	}{
		{"Empty Array", []int{}, []int{}},
		{"Single Element", []int{5}, []int{5}},
		{"Already Sorted (Ascending)", []int{1, 2, 3, 4, 5}, []int{1, 2, 3, 4, 5}},
		{"Already Sorted (Descending)", []int{5, 4, 3, 2, 1}, []int{1, 2, 3, 4, 5}},
		{"Random Order", []int{3, 1, 4, 1, 5, 9, 2, 6, 5, 3, 5}, []int{1, 1, 2, 3, 3, 4, 5, 5, 5, 6, 9}},
		{"Repeated Elements", []int{5, 2, 1, 5, 3, 2, 4, 1, 4, 3}, []int{1, 1, 2, 2, 3, 3, 4, 4, 5, 5}},
	}

	for _, tt := range tests {
		t.Run(tt.name, func(t *testing.T) {
			// Make a copy of the input array to compare with the output
			inputCopy := make([]int, len(tt.input))
			copy(inputCopy, tt.input)

			// Perform the quicksort
			quickSort(tt.input, 0, len(tt.input)-1)

			// Check if the array is sorted
			if !isSorted(tt.input) {
				t.Errorf("Array not sorted: %v", tt.input)
			}

			// Check if the output matches the expected result
			if !reflect.DeepEqual(tt.input, tt.output) {
				t.Errorf("Expected %v, got %v", tt.output, tt.input)
			}
		})
	}
}
