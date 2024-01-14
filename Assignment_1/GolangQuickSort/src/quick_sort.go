package main

func quickSort(array []int, l int, r int) {
	if l >= r {
		return
	}
	p := partition(array, l, r)
	quickSort(array, l, p-1)
	quickSort(array, p+1, r)
}

func partition(array []int, l int, r int) int {
	x := array[l]
	j := l
	for i := l + 1; i <= r; i++ {
		if array[i] <= x {
			j++
			array[j], array[i] = array[i], array[j]
		}
	}
	array[l], array[j] = array[j], array[l]
	return j
}
