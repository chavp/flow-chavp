<script lang="ts" setup>
interface TimeData {
    title: string,
    datetime: Date
}

defineProps<TimeData>()

</script>

<template>
    <div>Title: {{ title }}</div>
    <div>Time: {{ datetime }}</div>
</template>