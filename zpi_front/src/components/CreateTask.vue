<template>
  <div class="create-task">
    <div class="header">
      Podaj dane zadania
    </div>
    <div class="task-name wrapper">
      <div class="label">
        Nazwa:
      </div>
      <textarea ref="nameText"/>
    </div>
    <div class="task-description wrapper">
      <div class="label">
        Opis:
      </div>
      <textarea ref="descriptionText"/>
    </div>
    <div class="task-choose wrapper">
      <div class="label">
        Wykonawca:
      </div>
      <v-select v-model="selectedPerson" :options="teamUsersList" label="surname" class="style-chooser"/>
    </div>
    <div class="task-deadline wrapper">
      <div class="label">
        Deadline:
      </div>
      <textarea ref="deadlineText" :value="getNormalDate(date)" disabled/>
      <div class="date-picker-wrapper">
        <img
            src="https://www.nicepng.com/png/full/376-3766098_appointment-calendar-coming-soon-daily-date-datepicker-date.png"
            @click="toggleData()"/>
        <v-date-picker
            :value="null"
            color="green"
            is-dark
            class="picker"
            v-model="date"
            :class="showDataPicker ? 'visible' : ''"
        />
      </div>

    </div>
    <div class="task-choose wrapper">
      <div class="label">
        Ukończone:
      </div>
      <v-select v-model="taskDone" :options="[{option: 'Tak', id: 1}, {option: 'Nie', id: 2}]" label="option"
                class="style-chooser"/>
    </div>
    <div class="submit-button" @click="saveTask()">
      Zapisz
    </div>
  </div>
</template>

<script>
import vSelect from 'vue-select';

export default {
  name: "CreateTask",
  data() {
    return {
      taskDone: {option: 'Nie', id: 2},
      showDataPicker: false,
      date: new Date(2020, 1, 13),
      selectedPerson: null
    }
  },
  components: {
    vSelect
  },
  computed: {
    topicsList() {
      return this.$store.state.topics.topicsList
    },
    teamUsersList() {
      return this.$store.state.myTeam.teamUsersList
    }
  },
  methods: {
    saveTask() {
      let nameTextBox = this.$refs.nameText
      let descriptionTextBox = this.$refs.descriptionText
      let userId = this.selectedPerson ? this.selectedPerson.id : null;
      let deadline = this.getNormalDate(this.date)
      let taskDone = this.taskDone.id === 1

      this.$store.dispatch('myTeam/saveTask', {
        taskText: nameTextBox.value,
        descriptionText: descriptionTextBox.value,
        userId: userId,
        deadline: deadline,
        taskDone: taskDone
      })
    },
    toggleData() {
      this.showDataPicker = !this.showDataPicker
    },
    getNormalDate(date) {
      return date.getFullYear() + '-' + (date.getMonth() + 1) + '-' + date.getDate();
    },

  },
  mounted() {
    this.$store.dispatch('myTeam/getTeamUsers')
  }
}
</script>

<style scoped lang="scss">
.create-task {
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
    margin: 20px 0;

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
      padding: 15px;
      border: solid 1px var(--border-color-2);
      border-radius: 5px;
      overflow: hidden;
    }

    .date-picker-wrapper {
      flex: 4;
      display: flex;
      justify-content: center;
      position: relative;

      img {
        height: 40px;
        filter: invert(96%) sepia(10%) saturate(1236%) hue-rotate(61deg) brightness(99%) contrast(88%);
        cursor: pointer;
      }

      .picker {
        visibility: hidden;
        z-index: 1000;
        position: absolute;
        top: 50px;

        &.visible {
          visibility: visible;
        }
      }
    }

    .v-select {
      flex: 4;
    }

    &.task-name, &.task-choose, &.task-deadline {
      height: 50px;
    }


    &.task-description {
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