<script lang="ts" setup>
const props = defineProps(['title'])
const emit = defineEmits(['enlarge-text-event'])

</script>

<template>
  <h4>{{ props.title }}</h4>
  <button @click="emit('enlarge-text-event', {title: props.title})">Enlarge text</button>
</template>