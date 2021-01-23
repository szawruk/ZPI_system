<template>
  <div class="create-topic">
    <div class="header">
      Podaj dane tematu
    </div>
    <div class="topic-name wrapper">
      <div class="label">
        Temat:
      </div>
      <textarea ref="topicText"/>

    </div>
    <div class="topic-description wrapper">
      <div class="label">
        Opis:
      </div>
      <textarea ref="descriptionText"/>

    </div>
    <div class="submit-button" @click="saveTopic()">
      Zapisz
    </div>
  </div>
</template>

<script>
export default {
  name: "CreateTopic",
  methods: {
    saveTopic() {
      let topicTextBox = this.$refs.topicText
      let descriptionTextBox = this.$refs.descriptionText
      this.$store.dispatch('topics/saveTopic', {topicText: topicTextBox.value, descriptionText: descriptionTextBox.value})
    }
  }
}
</script>

<style scoped lang="scss">
.create-topic {
  width: 100%;
  display: flex;
  flex-direction: column;
  align-items: center;
  margin-top: 30px;
  background-color: rgba(255, 255, 255, 0.1);

  .header {
    width: 100%;
    height: 40px;
    font-size: 20px;
    display: flex;
    justify-content: center;
    align-items: center;
  }

  .header, .label {
    border: solid 2px var(--border-color-1);
    background-color: var(--bg-block-color);
  }

  .wrapper {
    width: 600px;
    display: flex;
    margin: 30px 0;

    .label {
      flex: 1;
      display: flex;
      justify-content: center;
      align-items: center;
      height: 50px;
      margin-right: 20px;
    }

    textarea {
      flex: 4;
      word-wrap: break-word;
      word-break: break-all;
      background-color: rgba(0, 0, 0, 0.4);
      resize: none;
      padding: 10px;
    }

    &.topic-name {
      height: 50px;
    }

    &.topic-description {
      height: 150px;
    }
  }

  .submit-button {
    position: fixed;
    bottom: 40px;
    right: 40px;
    color: var(--acefb9);
    border: solid 4px var(--border-color-1);
    width: 200px;
    height: 70px;
    display: flex;
    justify-content: center;
    align-items: center;
    font-weight: bold;
    font-size: 25px;
    background-color: rgba(172, 239, 185, 0.1);
    cursor: pointer;
    transition: all .2s ease-in-out;

    &:hover {
      background-color: rgba(172, 239, 185, 0.15);
    }
  }
}
</style>