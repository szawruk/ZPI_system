<template>
  <div class="create-team">
    <div class="header">
      Podaj dane zespołu
    </div>
    <div class="team-name wrapper">
      <div class="label">
        Nazwa:
      </div>
      <input ref="nameText"/>
    </div>
    <div class="team-description wrapper">
      <div class="label">
        Opis:
      </div>
      <textarea ref="descriptionText"/>
    </div>
    <div class="team-choose wrapper">
      <div class="label">
        Temat:
      </div>
      <v-select v-model="selected" :options="topicsList" label="name"  class="style-chooser"/>
    </div>

    <div class="submit-button" @click="saveTeam()">
      Zapisz
    </div>
  </div>
</template>

<script>
import vSelect from 'vue-select';

export default {
  name: "CreateTeam",
  data() {
    return {
      selected: null
    }
  },
  components: {
    vSelect
  },
  computed: {
    topicsList() {
      return this.$store.state.topics.topicsList
    }
  },
  methods: {
    saveTeam() {
      let topicId = this.selected ? this.selected.id : null;
      let nameTextBox = this.$refs.nameText
      let descriptionTextBox = this.$refs.descriptionText
      this.$store.dispatch('teams/saveTeam', {teamText: nameTextBox.value, descriptionText: descriptionTextBox.value, topicId: topicId})
    }
  },
  mounted() {
    this.$store.dispatch('topics/getTopicsList')
  }
}
</script>

<style scoped lang="scss">
.create-team {
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
      min-width: 120px;
      margin-right: 20px;
    }

    textarea {
      flex: 4;
      word-wrap: break-word;
      word-break: break-all;
      background-color: var(--bg-color);
      resize: none;
      padding: 10px;
      border: none;
      &:focus{
        outline: none !important;
        border:1px solid var(--acefb9);
        box-shadow: 0 0 5px var(--acefb9);
      }
    }

    .v-select {
      flex: 4;
    }



    &.team-name, .team-choose {
      height: 50px;
    }

    &.team-description {
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