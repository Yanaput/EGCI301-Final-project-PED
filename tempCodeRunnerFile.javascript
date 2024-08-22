class State {
    constructor(currentPrice, totalWorkNow, maxPrice, machineCount, selectedIdx, openedMachines) {
        this.currentPrice = currentPrice;
        this.totalWorkNow = totalWorkNow;
        this.maxPrice = maxPrice;
        this.machineCount = machineCount;
        this.selectedIdx = selectedIdx;
        this.openedMachines = openedMachines;
    }

    // Comparison function for priority queue
    compareTo(other) {
        return this.currentPrice - other.currentPrice;
    }
}

function selectMachines(machines, requiredJobs) {
    const pq = [];
    const start = new State(0, 0, 0, 0, 0, []);
    let finish = start;
    finish.currentPrice = Infinity;
    pq.push(start);

    while (pq.length > 0) {
        const lowestPriceState = pq.shift();

        const currentPrice = lowestPriceState.currentPrice;
        let totalWorkNow = lowestPriceState.totalWorkNow;
        let currentMaxPrice = lowestPriceState.maxPrice;
        let machineCount = lowestPriceState.machineCount;
        let selectedIdx = lowestPriceState.selectedIdx;
        const openedMachines = lowestPriceState.openedMachines;

        if (totalWorkNow >= requiredJobs && finish.currentPrice > currentPrice) {
            finish = lowestPriceState;
            continue;
        }
        if (selectedIdx === machines.length) {
            continue;
        }

        const noUse = new State(currentPrice, totalWorkNow, currentMaxPrice, machineCount, selectedIdx + 1, openedMachines);
        const use = new State(currentPrice, totalWorkNow, currentMaxPrice, machineCount, selectedIdx + 1, [...openedMachines, machines[selectedIdx]]);
        const currentMachine = machines[selectedIdx];
        use.maxPrice = Math.max(currentMaxPrice, currentMachine[0]);
        use.totalWorkNow += currentMachine[1];
        use.machineCount += 1;
        use.currentPrice = use.maxPrice * use.machineCount;

        pq.push(use);
        pq.push(noUse);
        pq.sort((a, b) => a.compareTo(b));
    }

    return finish;
}

const machines = [[10, 1], [21, 7], [32, 17], [15, 5], [30, 15], [25, 10]];
console.log("Machines: ", machines);
const requiredJobs = parseInt(prompt("Enter Required Job: "));
console.log("Required: ", requiredJobs);
const finalState = selectMachines(machines, requiredJobs);
console.log("Machines Used (Price, Work): ", finalState.openedMachines);
console.log("Total Costs: ", finalState.maxPrice);