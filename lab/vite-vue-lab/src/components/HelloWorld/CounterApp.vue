<script setup lang="ts">

defineProps(['count'])
//defineEmits(['increment-event']) 
const emit = defineEmits<{
    (e: 'increment-event', n: number, callback: Function):void
}>()

function incrementEventCallback(msg: String){
    console.log(msg)
}

//emit('increment-event', 10, incrementEventCallback)

</script>

<template>
    <button @click="$emit('increment-event', 1, incrementEventCallback)">Count App {{ count }}</button>
</template>