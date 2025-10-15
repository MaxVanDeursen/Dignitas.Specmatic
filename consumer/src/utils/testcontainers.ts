import { GenericContainer, StartedTestContainer } from "testcontainers";

let startedContainer: StartedTestContainer | undefined;

// Set up the Specmatic Testcontainer. This is executed through the globalSetup in vitest.config.ts.
export async function setup(project: { provide: (key: string, value: unknown) => void }): Promise<void> {
    startedContainer = await new GenericContainer("specmatic/specmatic:latest")
        .withExposedPorts(9000)
        .withBindMounts([
            { source: `${process.cwd()}/specmatic.yaml`, target: "/usr/src/app/specmatic.yaml" },
            { source: `${process.cwd()}/CartAPI_v1.yaml`, target: "/usr/src/app/CartAPI_v1.yaml" },
            { source: `${process.cwd()}/examples`, target: "/usr/src/app/examples" }
        ])
        .withCommand(["stub", "--examples=examples"])
        .withName("dignitas-specmatic-consumer-cart-mock")
        .start();
    project.provide("port", startedContainer.getMappedPort(9000));
}

export async function teardown(): Promise<void> {
    if (startedContainer) {
        await startedContainer.stop();
    }
}
