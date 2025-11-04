<script setup>
import { ref } from 'vue'
import { useMouse } from '../../composables/mouse.js'
import { useFetch } from '../../composables/fetch.js'

const { x, y } = useMouse()

const url = ref('/initial-url')
const { data, error } = useFetch(ref)

const vFocus = {
  mounted: (el) => el.focus()
}

</script>

<template>
    Mouse position is at: {{ x }}, {{ y }}
    <input v-focus />
    <div v-color="red">Test</div>
</template>